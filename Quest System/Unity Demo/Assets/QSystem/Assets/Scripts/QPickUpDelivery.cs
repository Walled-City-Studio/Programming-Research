using UnityEngine;

namespace QSystem
{
    public class QPickUpDelivery : MonoBehaviour
    {
        public CRITERIA_TYPE CriteriaType;

        public float InteractRadius = 3f;

        private SphereCollider SphereCollider;

        private QPackage QPackage;

        void Start()
        {
            AddSphereCollider();
        }

        void AddSphereCollider(Vector3? center = null)
        {
            SphereCollider = gameObject.AddComponent<SphereCollider>();
            SphereCollider.isTrigger = true;
            SphereCollider.center = center == null ? Vector3.zero : (Vector3)center;
            SphereCollider.radius = InteractRadius;
        }

        void OnTriggerEnter(Collider other)
        {
            if (QPackage == null)
            {
                Debug.Log("Package is not set for PickUp prefab or Delivery prefab/transform.");
                return;
            }

            if (other.gameObject.CompareTag("Player"))
            {
                // Pickup collider
                if (CriteriaType == CRITERIA_TYPE.PickUp)
                {
                    QHandler.Instance.PickUpPackage(QPackage);
                    Destroy(gameObject);
                }

                // Delivery collider
                else if (CriteriaType == CRITERIA_TYPE.Delivery)
                {
                    if(QPackage.PackageIsTaken)
                    {
                        Debug.Log(QPackage.PackageIsTaken);
                        QHandler.Instance.DeliverPackage(QPackage);
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
            QPackage = qPackage;
        }

    }
}
