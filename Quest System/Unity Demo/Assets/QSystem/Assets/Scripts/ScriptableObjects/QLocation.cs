using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestLocation", menuName = "Quest/Quest Location", order = 2)]
    public class QLocation : ScriptableObject
    {
        [SerializeField] public int BoatID;

        [SerializeField] public string LocationName;
        [SerializeField] public string LocationAdress;
        [SerializeField] public string LocationNotes;

        [SerializeField] public REGION_TYPE RegionType;

        [SerializeField] public Transform Location;
    }
}