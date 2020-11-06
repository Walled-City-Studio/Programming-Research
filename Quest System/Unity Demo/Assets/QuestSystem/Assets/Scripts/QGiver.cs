using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QGiver : MonoBehaviour
    {
        [SerializeField] private string Name;
        [SerializeField] private List<Quest> AvailableQuests;
     

        public void ListAvailableQuests()
        {
            //Create list popup for available quests
        }

        public void AcceptQuest(Quest ChosenQuest)
        {
            AvailableQuests.Remove(ChosenQuest);
        }
    }
}