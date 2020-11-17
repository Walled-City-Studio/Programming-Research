using UnityEngine;

namespace Code.Scripts.Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class WaterFloat : MonoBehaviour
    {
        //public properties
        [SerializeField] private float airDrag = 1;
        [SerializeField] private float waterDrag = 10;
        [SerializeField] private bool affectDirection = true;
        [SerializeField] private bool attachToSurface;
        [SerializeField] private Transform[] floatPoints;

        //used components
        private Rigidbody rigidbody;
        private Waves waves;

        //water line
        private float waterLine;
        private Vector3[] waterLinePoints;

        //help Vectors
        private Vector3 smoothVectorRotation;
        private Vector3 targetUp;
        private Vector3 centerOffset;

        private Vector3 Center
        {
            get { return transform.position + centerOffset; }
        }

        // Start is called before the first frame update
        private void Awake()
        {
            //get components
            waves = FindObjectOfType<Waves>();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = false;

            //compute center
            waterLinePoints = new Vector3[floatPoints.Length];
            for (int i = 0; i < floatPoints.Length; i++)
                waterLinePoints[i] = floatPoints[i].position;
            centerOffset = PhysicsHelper.GetCenter(waterLinePoints) - transform.position;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            //default water surface
            var newWaterLine = 0f;
            var pointUnderWater = false;
    
            //set WaterLinePoints and WaterLine
            for (int i = 0; i < floatPoints.Length; i++)
            {
                //height
                waterLinePoints[i] = floatPoints[i].position;
                waterLinePoints[i].y = waves.GetHeight(floatPoints[i].position);
                newWaterLine += waterLinePoints[i].y / floatPoints.Length;
                if (waterLinePoints[i].y > floatPoints[i].position.y)
                    pointUnderWater = true;
            }
    
            var waterLineDelta = newWaterLine - waterLine;
            waterLine = newWaterLine;
    
            //compute up vector
            targetUp = PhysicsHelper.GetNormal(waterLinePoints);
    
            //gravity
            var gravity = Physics.gravity;
            rigidbody.drag = airDrag;
            if (waterLine > Center.y)
            {
                rigidbody.drag = waterDrag;
                //under water
                if (attachToSurface)
                {
                    //attach to water surface
                    rigidbody.position = new Vector3(rigidbody.position.x, waterLine - centerOffset.y,
                        rigidbody.position.z);
                }
                else
                {
                    //go up
                    gravity = affectDirection ? targetUp * -Physics.gravity.y : -Physics.gravity;
                    transform.Translate(Vector3.up * (waterLineDelta * 0.9f));
                }
            }
    
            rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(waterLine - Center.y), 0, 1));
    
            //rotation
            if (pointUnderWater)
            {
                //attach to water surface
                targetUp = Vector3.SmoothDamp(transform.up, targetUp, ref smoothVectorRotation, 0.2f);
                rigidbody.rotation = Quaternion.FromToRotation(transform.up, targetUp) * rigidbody.rotation;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (floatPoints == null)
                return;

            for (int i = 0; i < floatPoints.Length; i++)
            {
                if (floatPoints[i] == null)
                    continue;

                if (waves != null)
                {
                    //draw cube
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(waterLinePoints[i], Vector3.one * 0.3f);
                }

                //draw sphere
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(floatPoints[i].position, 0.1f);
            }

            //draw center
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(new Vector3(Center.x, waterLine, Center.z), Vector3.one * 1f);
                Gizmos.DrawRay(new Vector3(Center.x, waterLine, Center.z), targetUp * 1f);
            }
        }
    }
}