using UnityEngine;

namespace QSystem
{
    public class QPickUpDelivery : MonoBehaviour
    {
        public CRITERIA_TYPE criteriaType;

        public float interactRadius = 3f;

        private SphereCollider sphereCollider;

        private QPackage qPackage;

        void Start()
        {
            AddSphereCollider();
        }

        void AddSphereCollider(Vector3? center = null)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.center = center == null ? Vector3.zero : (Vector3)center;
            sphereCollider.radius = interactRadius;
        }

        void OnTriggerEnter(Collider other)
        {
            if (qPackage == null)
            {
                Debug.Log("Package is not set for PickUp prefab or Delivery prefab/transform.");
                return;
            }

            if (other.gameObject.CompareTag("Player"))
            {
                // Pickup collider
                if (criteriaType == CRITERIA_TYPE.PICK_UP)
                {
                    QHandler.Instance.PickUpPackage(qPackage);
                    QHandler.Instance.InitQuestDelivery(qPackage);
                    Destroy(gameObject);
                }
                // Delivery collider
                else if (criteriaType == CRITERIA_TYPE.DELIVERY)
                {
                    if(qPackage.packageIsTaken)
                    {
                        QHandler.Instance.DeliverPackage(qPackage);
                        Destroy(gameObject);
                    }  
                    else
                    {
                        Debug.Log("PickUp package first before delivery.");
                    }
                } 
            }
        }

        public void SetPackage(QPackage qPackage)
        {
            this.qPackage = qPackage;
        }

    }
}
