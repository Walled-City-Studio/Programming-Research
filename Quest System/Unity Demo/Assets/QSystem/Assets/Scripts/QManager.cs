using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QSystem
{
    public class QManager : MonoBehaviour
    {
        public string asdasd = "awsdaasd";

        [SerializeField] private QuestInventory PlayerQuestInventory;

        private List<QGiver> QuestGivers = new List<QGiver>();

        // Start is called before the first frame update
        void Start()
        {
            QuestGivers.AddRange(FindObjectsOfType<QGiver>());
            PlayerQuestInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().QInventory;
        }

        // Update is called once per frame
        void Update()
        {
            //Add random quests to quest givers
        }

        public void GiveQuestToPlayer(QGiver QuestGiver, Quest AcceptedQuest)
        {
            if(PlayerQuestInventory.AddQuest(AcceptedQuest))
            {
                QuestGiver.RemoveQuest(AcceptedQuest);
            }
        }
    }
}