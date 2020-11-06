using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class QManager : MonoBehaviour
    {

        [SerializeField] private List<QGiver> QuestGivers;
        [SerializeField] private QuestInventory PlayerQuestInventory;
        // Start is called before the first frame update
        void Start()
        {
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