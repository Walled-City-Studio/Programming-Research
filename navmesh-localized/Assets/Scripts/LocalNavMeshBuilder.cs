using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

// Build and update a localized navmesh from the sources marked by NavMeshSourceTag
[DefaultExecutionOrder(-102)]
public class LocalNavMeshBuilder : MonoBehaviour
{
    // The center of the build
    [SerializeField]
    private Transform tracked;
    [SerializeField]
    private bool updateNavMesh;
    // The size of the build bounds
    [SerializeField]
    private Vector3 size = new Vector3(80.0f, 20.0f, 80.0f);

    NavMeshData navMesh;
    AsyncOperation operation;
    NavMeshDataInstance instance;
    List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();

    IEnumerator Start()
    {
        while (true)
        {
            UpdateNavMesh(updateNavMesh);
            yield return operation;
        }
    }

    void OnEnable()
    {
        // Construct and add navmesh
        navMesh = new NavMeshData();
        instance = NavMesh.AddNavMeshData(navMesh);
        if (tracked == null)
            tracked = transform;

        UpdateNavMesh(false);
    }

    void OnDisable()
    {
        // Unload navmesh and clear handle
        instance.Remove();
    }

    void UpdateNavMesh(bool asyncUpdate = false)
    {
        NavMeshSourceTag.Collect(ref sources);
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        var bounds = QuantizedBounds();

        if (asyncUpdate)
            operation = NavMeshBuilder.UpdateNavMeshDataAsync(navMesh, defaultBuildSettings, sources, bounds);
        else
            NavMeshBuilder.UpdateNavMeshData(navMesh, defaultBuildSettings, sources, bounds);
    }

    static Vector3 Quantize(Vector3 v, Vector3 quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }

    Bounds QuantizedBounds()
    {
        // Quantize the bounds to update only when theres a 10% change in size
        var center = tracked ? tracked.position : transform.position;
        return new Bounds(Quantize(center, 0.1f * size), size);
    }

    void OnDrawGizmosSelected()
    {
        if (navMesh)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(navMesh.sourceBounds.center, navMesh.sourceBounds.size);
        }

        Gizmos.color = Color.yellow;
        var bounds = QuantizedBounds();
        Gizmos.DrawWireCube(bounds.center, bounds.size);

        Gizmos.color = Color.green;
        var center = tracked ? tracked.position : transform.position;
        Gizmos.DrawWireCube(center, size);
    }
}
