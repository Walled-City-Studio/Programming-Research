using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestLocation", menuName = "Quest/Quest Location", order = 2)]
    public class QLocation : ScriptableObject
    {
        [SerializeField] 
        public int boatID;

        [SerializeField] 
        public string locationName;
        [SerializeField] 
        public string locationAdress;
        [SerializeField] 
        public string locationNotes;

        [SerializeField] 
        public REGION_TYPE regionType;

        [SerializeField] 
        public Transform location;
    }
}