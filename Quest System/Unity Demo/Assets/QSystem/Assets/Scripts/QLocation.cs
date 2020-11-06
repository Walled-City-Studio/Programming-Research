using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestLocation", menuName = "Quest/QuestLocation", order = 0)]
    public class QLocation : ScriptableObject
    {
        public int BoatID;

        public string CustomerName;
        public string CustomerAdress;
        public string CustomerNotes;

        public REGION_TYPE RegionType;

        public Vector3 Location;
    }
}