using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private string Title;
        [SerializeField] private string Description;

        [SerializeField] private Package Package;
        [SerializeField] private QGiver Giver;
        [SerializeField] private QReward Reward;
        [SerializeField] private float MaxDeliverTime;
        private float StartTime;


        public void Start()
        {
            Reward = new QReward(MaxDeliverTime);
            StartTime = Time.time;
        }

        public QReward FinishQuest()
        {
            if(Package.IsDelivered())
            {
                Reward.StopTimerAt(Time.time - StartTime);
                return Reward;
            }
            else
            {
                return null;
            }

        }
    }
}
