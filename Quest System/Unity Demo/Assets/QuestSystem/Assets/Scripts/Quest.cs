using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private string title;
        [SerializeField] private string description;

        [SerializeField] private Package package;
        public List<QGiver> qGivers = new List<QGiver>();
        public QReward qReward;

        public void Start()
        {
            //Display description in dialog/pop-up
        }

        public void AcceptQuest(bool acceptance)
        {
            //Add quest to quest list if accepted
        }

        public void FinishQuest()
        {
            //If criteria are met give out reward and delete quest
        }
    }
}
