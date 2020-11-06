using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class QGiver : MonoBehaviour
    {
        [SerializeField] private float Radius = 3;
        [SerializeField] private List<Quest> Quests;
        
        private SphereCollider SphereCollider;

        private void Start()
        {
            SetSphereCollider();
        }

        void SetSphereCollider()
        {
            SphereCollider = gameObject.AddComponent<SphereCollider>();
            SphereCollider.isTrigger = true;
            SphereCollider.center = Vector3.zero;
            SphereCollider.radius = Radius;
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}