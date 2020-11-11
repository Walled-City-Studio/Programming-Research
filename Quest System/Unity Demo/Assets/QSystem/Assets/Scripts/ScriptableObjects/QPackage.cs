using Boo.Lang;
using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestPackage", menuName = "Quest/Quest Package", order = 1)]
    public class QPackage : ScriptableObject
    {
        [SerializeField] 
        public LEGAL_STATUS legalStatus;
        [SerializeField] 
        public PACKAGE_TYPE type;

        [SerializeField] 
        float weight;

        [SerializeField] 
        string description;

        [SerializeField] 
        public QLocation pickUpLocation;
        [SerializeField] 
        public QLocation deliveryLocation;

        [SerializeField] 
        public GameObject pickUpPrefab;
        [SerializeField] 
        public GameObject deliveryPrefab;

        public bool packageIsTaken = false;
        public bool packageIsDelivered = false;

        public void OnEnable()
        {
            packageIsTaken = false;
            packageIsDelivered = false;
        }

    }
}