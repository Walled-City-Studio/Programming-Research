using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Environment;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterFloat : MonoBehaviour
{
    //public properties
    public float airDrag = 1;
    public float waterDrag = 10;
    public bool affectDirection = true;
    public bool attachToSurface;
    public Transform[] floatPoints;

    //used components
    private Rigidbody _rigidbody;
    private Waves _waves;

    //water line
    private float _waterLine;
    private Vector3[] _waterLinePoints;

    //help Vectors
    private Vector3 _smoothVectorRotation;
    private Vector3 _targetUp;
    private Vector3 _centerOffset;

    public Vector3 Center
    {
        get { return transform.position + _centerOffset; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //get components
        _waves = FindObjectOfType<Waves>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        //compute center
        _waterLinePoints = new Vector3[floatPoints.Length];
        for (int i = 0; i < floatPoints.Length; i++)
            _waterLinePoints[i] = floatPoints[i].position;
        _centerOffset = PhysicsHelper.GetCenter(_waterLinePoints) - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //default water surface
        var newWaterLine = 0f;
        var pointUnderWater = false;
    
        //set WaterLinePoints and WaterLine
        for (int i = 0; i < floatPoints.Length; i++)
        {
            //height
            _waterLinePoints[i] = floatPoints[i].position;
            _waterLinePoints[i].y = _waves.GetHeight(floatPoints[i].position);
            newWaterLine += _waterLinePoints[i].y / floatPoints.Length;
            if (_waterLinePoints[i].y > floatPoints[i].position.y)
                pointUnderWater = true;
        }
    
        var waterLineDelta = newWaterLine - _waterLine;
        _waterLine = newWaterLine;
    
    //compute up vector
    _targetUp = PhysicsHelper.GetNormal(_waterLinePoints);
    
    //gravity
    var gravity = Physics.gravity;
    _rigidbody.drag = airDrag;
    if (_waterLine > Center.y)
    {
        _rigidbody.drag = waterDrag;
        //under water
        if (attachToSurface)
        {
            //attach to water surface
            _rigidbody.position = new Vector3(_rigidbody.position.x, _waterLine - _centerOffset.y,
                _rigidbody.position.z);
        }
        else
        {
            //go up
            gravity = affectDirection ? _targetUp * -Physics.gravity.y : -Physics.gravity;
            transform.Translate(Vector3.up * (waterLineDelta * 0.9f));
        }
    }
    
    _rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(_waterLine - Center.y), 0, 1));
    
    //rotation
    if (pointUnderWater)
    {
        //attach to water surface
        _targetUp = Vector3.SmoothDamp(transform.up, _targetUp, ref _smoothVectorRotation, 0.2f);
        _rigidbody.rotation = Quaternion.FromToRotation(transform.up, _targetUp) * _rigidbody.rotation;
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

            if (_waves != null)
            {
                //draw cube
                Gizmos.color = Color.red;
                Gizmos.DrawCube(_waterLinePoints[i], Vector3.one * 0.3f);
            }

            //draw sphere
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(floatPoints[i].position, 0.1f);
        }

        //draw center
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, _waterLine, Center.z), Vector3.one * 1f);
            Gizmos.DrawRay(new Vector3(Center.x, _waterLine, Center.z), _targetUp * 1f);
        }
    }
}