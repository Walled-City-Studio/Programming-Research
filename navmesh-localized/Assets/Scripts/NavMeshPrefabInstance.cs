using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[ExecuteInEditMode]
[DefaultExecutionOrder(-102)]
public class NavMeshPrefabInstance : MonoBehaviour
{
    [SerializeField]
    NavMeshData navMesh;
    public NavMeshData navMeshData
    {
        get { return navMesh; }
        set { navMesh = value; }
    }

    [SerializeField]
    bool followTransformToggle;
    public bool followTransform
    {
        get { return followTransformToggle; }
        set { SetFollowTransform(value); }
    }

    NavMeshDataInstance instance;

    // Position Tracking
    static readonly List<NavMeshPrefabInstance> trackedInstancesList = new List<NavMeshPrefabInstance>();
    public static List<NavMeshPrefabInstance> trackedInstances {get {return trackedInstancesList; }}
    Vector3 position;
    Quaternion rotation;

    void OnEnable()
    {
        AddInstance();

        if (instance.valid && followTransformToggle)
            AddTracking();
    }

    void OnDisable()
    {
        instance.Remove();
        RemoveTracking();
    }

    public void UpdateInstance()
    {
        instance.Remove();
        AddInstance();
    }

    void AddInstance()
    {
#if UNITY_EDITOR
        if (instance.valid)
        {
            Debug.LogError("Instance is already added: " + this);
            return;
        }
#endif
        if (navMesh)
            instance = NavMesh.AddNavMeshData(navMesh, transform.position, transform.rotation);

        rotation = transform.rotation;
        position = transform.position;
    }

    void AddTracking()
    {
#if UNITY_EDITOR
        // At runtime we don't want linear lookup
        if (trackedInstances.Contains(this))
        {
            Debug.LogError("Double registration of " + this);
            return;
        }
#endif
        if (trackedInstances.Count == 0)
            NavMesh.onPreUpdate += UpdateTrackedInstances;
        trackedInstances.Add(this);
    }

    void RemoveTracking()
    {
        trackedInstances.Remove(this);
        if (trackedInstances.Count == 0)
            NavMesh.onPreUpdate -= UpdateTrackedInstances;
    }

    void SetFollowTransform(bool value)
    {
        if (followTransformToggle == value)
            return;
        followTransformToggle = value;
        if (value)
            AddTracking();
        else
            RemoveTracking();
    }

    bool HasMoved()
    {
        return position != transform.position || rotation != transform.rotation;
    }

    static void UpdateTrackedInstances()
    {
        foreach (var instance in trackedInstances)
        {
            if (instance.HasMoved())
                instance.UpdateInstance();
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Only when the instance is valid (OnEnable is called) - we react to changes caused by serialization
        if (!instance.valid)
            return;
        // OnValidate can be called several times - avoid double registration
        // We afford this linear lookup in the editor only
        if (!followTransformToggle)
        {
            RemoveTracking();
        }
        else if (!trackedInstances.Contains(this))
        {
            AddTracking();
        }
    }
#endif
}
