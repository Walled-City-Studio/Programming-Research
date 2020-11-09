using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Code.Scripts.Environment
{
    public class Waves : MonoBehaviour
    {
        public int dimension = 10;
        public Octave[] octaves;
        public int uvScale = 1;

        private MeshFilter _meshFilter;
        private Mesh _mesh;

        public float GetHeight(Vector3 position)
        {
            //Scale factor and position in local space
            var lossyScale = transform.lossyScale;
            var scale = new Vector3(1 / lossyScale.x, 0, 1 / lossyScale.z);
            var localPos = Vector3.Scale((position - transform.position), scale);

            //Get edge points
            var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
            var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
            var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
            var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));

            //Clamp if the position is outside the plane
            p1.x = Mathf.Clamp(p1.x, 0, dimension);
            p1.z = Mathf.Clamp(p1.z, 0, dimension);
            p2.x = Mathf.Clamp(p2.x, 0, dimension);
            p2.z = Mathf.Clamp(p2.z, 0, dimension);
            p3.x = Mathf.Clamp(p3.x, 0, dimension);
            p3.z = Mathf.Clamp(p3.z, 0, dimension);
            p4.x = Mathf.Clamp(p4.x, 0, dimension);
            p4.z = Mathf.Clamp(p4.z, 0, dimension);

            //Get the max distance to one of the edges and take that to compute max - dist
            var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos),
                Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            var dist = (max - Vector3.Distance(p1, localPos)) +
                            (max - Vector3.Distance(p2, localPos)) +
                            (max - Vector3.Distance(p3, localPos)) +
                            (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            
            //Weighted sum
            var height = _mesh.vertices[Index((int) p1.x, (int) p1.z)].y * (max - Vector3.Distance(p1, localPos)) +
                         _mesh.vertices[Index((int) p2.x, (int) p2.z)].y * (max - Vector3.Distance(p2, localPos)) +
                         _mesh.vertices[Index((int) p3.x, (int) p3.z)].y * (max - Vector3.Distance(p3, localPos)) +
                         _mesh.vertices[Index((int) p4.x, (int) p4.z)].y * (max - Vector3.Distance(p4, localPos));
            
            //Scale
            return height * transform.lossyScale.y / dist;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _mesh = new Mesh();
            _mesh.name = gameObject.name;

            _mesh.vertices = GenerateVertices();
            _mesh.triangles = GenerateTriangles();
            _mesh.uv = GenerateUVs();

            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();

            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshFilter.mesh = _mesh;
        }

        private Vector2[] GenerateUVs()
        {
            var uvs = new Vector2[_mesh.vertices.Length];

            //Always set one UV over n tiles then flip the UV and set it again
            for (int x = 0; x <= dimension; x++)
            {
                for (int z = 0; z <= dimension; z++)
                {
                    var vector = new Vector2((x / uvScale) % 2, (z / uvScale) % 2);
                    uvs[Index(x, z)] = new Vector2(vector.x <= 1 ? vector.x : 2 - vector.x,
                        vector.y <= 1 ? vector.y : 2 - vector.y);
                }
            }

            return uvs;
        }

        private int[] GenerateTriangles()
        {
            var triangles = new int[_mesh.vertices.Length * 6];

            //Two triangles per tile
            for (int x = 0; x < dimension; x++)
            {
                for (int z = 0; z < dimension; z++)
                {
                    triangles[Index(x, z) * 6 + 0] = Index(x, z);
                    triangles[Index(x, z) * 6 + 1] = Index(x + 1, z + 1);
                    triangles[Index(x, z) * 6 + 2] = Index(x + 1, z);
                    triangles[Index(x, z) * 6 + 3] = Index(x, z);
                    triangles[Index(x, z) * 6 + 4] = Index(x, z + 1);
                    triangles[Index(x, z) * 6 + 5] = Index(x + 1, z + 1);
                }
            }

            return triangles;
        }

        private Vector3[] GenerateVertices()
        {
            var vertices = new Vector3[(dimension + 1) * (dimension + 1)];

            //Equally distributed vertices
            for (int x = 0; x <= dimension; x++)
            {
                for (int z = 0; z <= dimension; z++)
                {
                    vertices[Index(x, z)] = new Vector3(x, 0, z);
                }
            }

            return vertices;
        }

        private int Index(int x, int z)
        {
            return x * (dimension + 1) + z;
        }

        // Update is called once per frame
        private void Update()
        {
            var vertices = _mesh.vertices;

            for (int x = 0; x <= dimension; x++)
            {
                for (int z = 0; z <= dimension; z++)
                {
                    float y = 0f;

                    for (int i = 0; i < octaves.Length; i++)
                    {
                        if (octaves[i].alternate)
                        {
                            var perl = Mathf.PerlinNoise(
                                (x * octaves[i].scale.x) / dimension,
                                (z * octaves[i].scale.y) / dimension) * Mathf.PI * 2f;
                            y += Mathf.Cos(perl + octaves[i].speed.magnitude * Time.time) * octaves[i].height;
                        }
                        else
                        {
                            var perl = Mathf.PerlinNoise(
                                (x * octaves[i].scale.x + Time.time * octaves[i].speed.x) / dimension,
                                (z * octaves[i].scale.y + Time.time * octaves[i].speed.y) / dimension) - 0.5f;
                            y += perl * octaves[i].height;
                        }
                    }

                    vertices[Index(x, z)] = new Vector3(x, y, z);
                }
            }

            _mesh.vertices = vertices;
            _mesh.RecalculateNormals();
        }

        [Serializable]
        public struct Octave
        {
            public Vector2 speed;
            public Vector2 scale;
            public float height;
            public bool alternate;
        }
    }
}