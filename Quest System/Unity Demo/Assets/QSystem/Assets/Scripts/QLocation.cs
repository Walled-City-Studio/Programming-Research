using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestLocation", menuName = "Quest/QuestLocation", order = 0)]
    public class QLocation : ScriptableObject
    {
        public int BoatID;

        public string LocationName;
        public string LocationAdress;
        public string LocationNotes;

        public REGION_TYPE RegionType;

        public Vector3 Location;
    }
}