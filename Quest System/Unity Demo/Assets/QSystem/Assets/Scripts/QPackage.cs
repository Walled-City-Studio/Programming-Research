using UnityEngine;

namespace QSystem
{
    public class QPackage : MonoBehaviour
    {
        [SerializeField] private QLocation PickUpLocation;
        [SerializeField] private QLocation DeliveryLocation;

        [SerializeField] private LEGAL_STATUS LegalStatus;

        [SerializeField] private int Size;

        [SerializeField] private GameObject PackagePrefab;
    }
}