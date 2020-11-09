using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private string Title;
        [SerializeField] private string Description;

        [SerializeField] private QReward QReward;

        [SerializeField] public Dialogue Dialogue;

<<<<<<< HEAD
        public QPackage QPackage;
       
=======
        [SerializeField] private QPackage QPackage;
>>>>>>> parent of 9c55c73... Latest
        [SerializeField] private float MaxDeliverTime;

        private float StartTime;

        public void Start()
        {
            QReward = new QReward(MaxDeliverTime);
            StartTime = Time.time;
        }

        public QReward FinishQuest()
        {
            if (QPackage.IsDelivered())
            {
                QReward.StopTimerAt(Time.time - StartTime);
                return QReward;
            }
            else
            {
                return null;
            }

        }
    }
}

