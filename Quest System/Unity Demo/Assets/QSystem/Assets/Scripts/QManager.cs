<<<<<<< HEAD
<<<<<<< HEAD
﻿using System.Collections;
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
            //PlayerQuestInventory = GetComponent<PlayerController>().QInventory;
            QuestGivers.AddRange(FindObjectsOfType<QGiver>());
        }

        // Update is called once per frame
        void Update()
        {
            //Add random quests to quest givers
        }

        public void GiveQuestToPlayer(QGiver QuestGiver, Quest AcceptedQuest)
        {
            QuestGiver.RemoveQuest(AcceptedQuest);
            PlayerQuestInventory.AddQuest(AcceptedQuest);
        }
    }
=======
﻿using System.Collections;
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
            //PlayerQuestInventory = GetComponent<PlayerController>().QInventory;
            QuestGivers.AddRange(FindObjectsOfType<QGiver>());
        }

        // Update is called once per frame
        void Update()
        {
            //Add random quests to quest givers
        }

        public void GiveQuestToPlayer(QGiver QuestGiver, Quest AcceptedQuest)
        {
            QuestGiver.RemoveQuest(AcceptedQuest);
            PlayerQuestInventory.AddQuest(AcceptedQuest);
        }
    }
>>>>>>> parent of 9c55c73... Latest
=======
﻿using System.Collections;
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
            //PlayerQuestInventory = GetComponent<PlayerController>().QInventory;
            QuestGivers.AddRange(FindObjectsOfType<QGiver>());
        }

        // Update is called once per frame
        void Update()
        {
            //Add random quests to quest givers
        }

        public void GiveQuestToPlayer(QGiver QuestGiver, Quest AcceptedQuest)
        {
            QuestGiver.RemoveQuest(AcceptedQuest);
            PlayerQuestInventory.AddQuest(AcceptedQuest);
        }
    }
>>>>>>> parent of 9c55c73... Latest
}