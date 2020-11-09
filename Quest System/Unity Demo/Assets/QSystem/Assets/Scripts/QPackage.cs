using UnityEngine;

namespace QSystem
{
    public class QPackage : MonoBehaviour
    {
        [SerializeField] private QLocation PickUpLocation;
        [SerializeField] private QLocation DeliveryLocation;

        [SerializeField] private LEGAL_STATUS LegalStatus;

<<<<<<< HEAD
        public PACKAGE_SIZE Size
        {
            get { return Size; }
            private set { Size = value; }
        }
=======
        [SerializeField] private PACKAGE_SIZE Size;
>>>>>>> parent of 9c55c73... Latest

        [SerializeField] private GameObject PackagePrefab;
        [SerializeField] private float MaxInteractionDistanceFromLocation;

        public bool IsDelivered()
        {
            return (Vector3.Distance(DeliveryLocation.Location, transform.position) <= MaxInteractionDistanceFromLocation);
        }
    }
}