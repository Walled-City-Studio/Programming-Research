using UnityEngine;

namespace QSystem
{
    public class QPackage : MonoBehaviour
    {
        

        [SerializeField] private QLocation PickUpLocation;
        [SerializeField] private QLocation DeliveryLocation;

        [SerializeField] private LEGAL_STATUS LegalStatus;

        [SerializeField] private PACKAGE_SIZE Size;

        [SerializeField] private GameObject PackagePrefab;
        [SerializeField] private float MaxInteractionDistanceFromLocation;

        public bool IsDelivered()
        {
            return (Vector3.Distance(DeliveryLocation.Location, transform.position) <= MaxInteractionDistanceFromLocation);
        }
    }
}