using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class QHandler : Manager<QHandler>
    {
        private QInventory qInventory;

        private List<Quest> acceptedQuests = new List<Quest>();
        private List<Quest> completedQuests = new List<Quest>();
        private List<Quest> failedQuests = new List<Quest>(); 
        private List<Quest> discoveredQuests = new List<Quest>();

        private List<GameObject> questObjects = new List<GameObject>();

        private Quest currentQuest;
        private Quest currentDialogueQuest;

        private QGiver currentDialogueQuestGiver;

        private bool showQuestCounter = false;

        private int questMaxTime = 0;

        void Start()
        {
            qInventory = FindObjectOfType<PlayerController>().QInventory;
        }

        void Update()
        {
            if(showQuestCounter)
            {
                StartCoroutine(StartQuestCounter(questMaxTime));
            }
        }

        // Called from Button event
        public void AcceptQuestFromDiaglogue()
        {
            if(IsDialogueSet())
            {
                if(currentQuest == null)
                {
                    AcceptQuest(currentDialogueQuest);
                }
                else
                {
                    Debug.Log("There is another quest in progress.");
                }
            }
            else
            {
                Debug.Log("No current quest diaglogue is set.");
            }
        }

        public void InitQuestTimer(Quest quest)
        {      
            questMaxTime = (int)quest.maxDeliverTime;
            showQuestCounter = true;
            QGUIOverlay.Instance.SetQuestMaxTime(quest.maxDeliverTime);
            QGUIOverlay.Instance.ShowQuestTimer(true);
        }

        IEnumerator StartQuestCounter(int maxSeconds)
        {
            yield return StartCoroutine(StartCounter(maxSeconds));

            // When timer reaches max delivery time and quest isn't completed, fail quest 
            if (currentQuest != null && currentQuest.QuestStatus != QUEST_STATUS.COMPLETE)
            {
                FailQuest(currentQuest);
            }
        }

        IEnumerator StartCounter(int maxSeconds)
        {
            for (int i = 0; i < maxSeconds; i++)
            {
                QGUIOverlay.Instance.SetQuestTotalTime(i);
                yield return new WaitForSeconds(1);
            }
        }

        private void AcceptQuest(Quest quest)
        {
            Debug.Log("Accept quest");
            currentQuest = quest;
            acceptedQuests.Add(quest);
            currentDialogueQuestGiver.RemoveQuest(quest);
            SetQuestStatus(quest, QUEST_STATUS.ACCEPT);
            SetQuestStartTime(quest);
            InitQuestPickUp(quest.qPackage);
            InitQuestDelivery(quest.qPackage);
            InitQuestTimer(quest);
        }

        private void FailQuest(Quest quest)
        {
            if (quest.maxDeliverTime <= QGUIOverlay.Instance.GetCurrentCount() + 1)
            {
                Debug.Log("Fail quest");
                RoundQuest(quest, QUEST_STATUS.FAIL);
            }
        }

        private void CompleteQuest(Quest quest)
        {
            if (quest.qPackage.packageIsDelivered && quest.qPackage.packageIsTaken)
            {
                Debug.Log("Complete quest");
                RoundQuest(quest, QUEST_STATUS.COMPLETE);
            }  
        }

        private void RoundQuest(Quest quest, QUEST_STATUS status)
        {
            if (acceptedQuests.Contains(quest))
            {
                if (currentQuest == quest)
                {
                    SetQuestEndTime(quest);
                    SetQuestStatus(quest, status);
                    acceptedQuests.Remove(quest);
                    qInventory.RemoveQuestPackage(quest.qPackage);

                    if (status == QUEST_STATUS.COMPLETE)
                    {
                        completedQuests.Add(quest);
                        qInventory.AddQuestReward(quest.qReward, quest.scaleReward, quest.challengeType);
                    }

                    else if (status == QUEST_STATUS.FAIL)
                    {
                        DespawnQuestPrefabs();
                        failedQuests.Add(quest);
                    }

                    QGUIOverlay.Instance.ShowQuestTimer(false);
                    QGUIOverlay.Instance.ResetTimerValues();
                    showQuestCounter = false;
                    currentQuest = null;
                    questMaxTime = 0;
                }
                else
                {
                    Debug.Log("It's not allowed to round other quests then the current quest.");
                }
            }
            else
            {
                Debug.Log("It's not allowed to round quests that aren't accepted.");
            }
        }

        private void DespawnQuestPrefabs()
        {
            foreach(GameObject qObject in questObjects)
            {
                Destroy(qObject);
            }
        }

        public void SetQuestStatus(Quest quest, QUEST_STATUS status)
        {
            quest.QuestStatus = status;
        }

        public void SetQuestStartTime(Quest quest)
        {
            quest.startTime = Time.time;
        }

        public void SetQuestEndTime(Quest quest)
        {
            quest.endTime = Time.time;
            quest.totalTime = quest.endTime - quest.startTime;
        }

        public bool IsDialogueSet()
        {
            return currentDialogueQuest != null && currentDialogueQuestGiver != null;
        }

        public void InitQuestPickUp(QPackage package)
        {
            GameObject pickUp = Instantiate(
                package.pickUpPrefab,
                package.pickUpLocation.location.position,
                package.pickUpLocation.location.rotation);

            pickUp.GetComponent<QPickUpDelivery>().SetPackage(package);
            questObjects.Add(pickUp);
        }
               
        public void InitQuestDelivery(QPackage package)
        {            
            GameObject delivery = Instantiate(
                package.deliveryPrefab,
                package.deliveryLocation.location.position,
                package.deliveryLocation.location.rotation);

            delivery.GetComponent<QPickUpDelivery>().SetPackage(package);
            questObjects.Add(delivery);
        }

        public void PickUpPackage(QPackage package)
        {
            package.packageIsTaken = true;
            qInventory.AddQuestPackage(package);
        }

        // TODO: CompleteQuest should not be called from this method
        public void DeliverPackage(QPackage package)
        {
            package.packageIsDelivered = true;
            qInventory.RemoveQuestPackage(package);
            CompleteQuest(currentQuest);
        }

        public void SetCurrentDiaglogueQuest(Quest quest = null, QGiver qGiver = null)
        {
            currentDialogueQuest = quest;
            currentDialogueQuestGiver = qGiver;
        }

        public void AddQuestDiscovered(Quest quest)
        {
            if(!discoveredQuests.Contains(quest))
            {
                discoveredQuests.Add(quest);
            }
        }

    }
}