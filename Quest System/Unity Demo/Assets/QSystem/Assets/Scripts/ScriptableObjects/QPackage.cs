using Boo.Lang;
using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestPackage", menuName = "Quest/Quest Package", order = 1)]
    public class QPackage : ScriptableObject
    {
        [SerializeField] public LEGAL_STATUS LegalStatus;
        [SerializeField] public PACKAGE_SIZE Size;
        [SerializeField] public DELIVERY_TYPE DeliveryType;

        [SerializeField] public QLocation PickUpLocation;
        [SerializeField] public QLocation DeliveryLocation;

        [SerializeField] public GameObject PickUpPrefab;
        [SerializeField] public GameObject DeliveryPrefab;

        [SerializeField] public Transform DeliveryTransform;

        public bool PackageIsTaken = false;
        public bool PackageIsDelivered = false;

        public void OnEnable()
        {
            PackageIsTaken = false;
            PackageIsDelivered = false;
        }

    }
}