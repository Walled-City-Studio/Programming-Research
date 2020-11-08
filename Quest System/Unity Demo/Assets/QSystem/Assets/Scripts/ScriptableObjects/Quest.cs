using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] public Dialogue Dialogue;

        [SerializeField] public QPackage QPackage;

        [SerializeField] public string Title;
        [SerializeField] public string Description;

        [SerializeField] public QReward QReward;

        [SerializeField] public float MaxDeliverTime;

        [SerializeField] public CHALLENGE_TYPE ChallengeType;

        [SerializeField] public bool ScaleRewardChallenge;

        public float StartTime;
        public float EndTime;
        public float TotalTime;

        public QUEST_STATUS QuestStatus = QUEST_STATUS.Default;
    }
}

