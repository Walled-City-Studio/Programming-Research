using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// Tagging component for use with the LocalNavMeshBuilder
// Supports mesh-filter and terrain - can be extended to physics and/or primitives
[DefaultExecutionOrder(-200)]
public class NavMeshSourceTag : MonoBehaviour
{
    // Global containers for all active mesh/terrain tags
    public static List<MeshFilter> meshes = new List<MeshFilter>();
    public static List<Terrain> terrains = new List<Terrain>();

    void OnEnable()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshes.Add(meshFilter);
        }

        var terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrains.Add(terrain);
        }
    }

    void OnDisable()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshes.Remove(meshFilter);
        }

        var terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrains.Remove(terrain);
        }
    }

    // Collect all the navmesh build sources for enabled objects tagged by this component
    public static void Collect(ref List<NavMeshBuildSource> sources)
    {
        sources.Clear();

        for (var i = 0; i < meshes.Count; ++i)
        {
            var meshFilter = meshes[i];
            if (meshFilter == null) continue;

            var mesh = meshFilter.sharedMesh;
            if (mesh == null) continue;

            var navMeshBuildSource = new NavMeshBuildSource();
            navMeshBuildSource.shape = NavMeshBuildSourceShape.Mesh;
            navMeshBuildSource.sourceObject = mesh;
            navMeshBuildSource.transform = meshFilter.transform.localToWorldMatrix;
            navMeshBuildSource.area = 0;
            sources.Add(navMeshBuildSource);
        }

        for (var i = 0; i < terrains.Count; ++i)
        {
            var terrain = terrains[i];
            if (terrain == null) continue;

            var navMeshBuildSource = new NavMeshBuildSource();
            navMeshBuildSource.shape = NavMeshBuildSourceShape.Terrain;
            navMeshBuildSource.sourceObject = terrain.terrainData;
            // Terrain system only supports translation - so we pass translation only to back-end
            navMeshBuildSource.transform = Matrix4x4.TRS(terrain.transform.position, Quaternion.identity, Vector3.one);
            navMeshBuildSource.area = 0;
            sources.Add(navMeshBuildSource);
        }
    }
}
