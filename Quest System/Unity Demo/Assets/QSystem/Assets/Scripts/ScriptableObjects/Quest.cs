using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] 
        public Dialogue dialogue;

        [SerializeField] 
        public QPackage qPackage;

        [SerializeField] 
        public string title;
        [SerializeField] 
        public string description;

        [SerializeField] 
        public QReward qReward;

        [SerializeField] 
        public float maxDeliverTime;

        [SerializeField] 
        public CHALLENGE_TYPE challengeType;

        [SerializeField] 
        public bool scaleReward;

        public float startTime;
        public float endTime;
        public float totalTime;

        public QUEST_STATUS QuestStatus = QUEST_STATUS.DEFAULT;
    }
}

