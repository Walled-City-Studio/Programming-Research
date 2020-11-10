using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class QHandler : Manager<QHandler>
    {
        private QInventory QInventory;

        private List<Quest> AcceptedQuests = new List<Quest>();
        private List<Quest> CompletedQuests = new List<Quest>();
        private List<Quest> FailedQuests = new List<Quest>(); 
        private List<Quest> DiscoveredQuests = new List<Quest>();

        private List<GameObject> QuestObjects = new List<GameObject>();

        private Quest CurrentQuest;
        private Quest CurrentDialogueQuest;

        private QGiver CurrentDialogueQuestGiver;

        private bool ShowQuestCounter = false;

        private int QuestMaxTime = 0;

        void Start()
        {
            QInventory = FindObjectOfType<PlayerController>().QInventory;
        }

        void Update()
        {
            if(ShowQuestCounter)
            {
                StartCoroutine(StartQuestCounter(QuestMaxTime));
            }
        }

        // Called from Button event
        public void AcceptQuestFromDiaglogue()
        {
            if(IsDialogueSet())
            {
                if(CurrentQuest == null)
                {
                    AcceptQuest(CurrentDialogueQuest);
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
            QuestMaxTime = (int)quest.MaxDeliverTime;
            ShowQuestCounter = true;
            QGUIOverlay.Instance.SetQuestMaxTime(quest.MaxDeliverTime);
            QGUIOverlay.Instance.ShowQuestTimer(true);
        }

        IEnumerator StartQuestCounter(int maxSeconds)
        {
            yield return StartCoroutine(StartCounter(maxSeconds));

            // When timer reaches max delivery time and quest isn't completed, fail quest 
            if (CurrentQuest != null && CurrentQuest.QuestStatus != QUEST_STATUS.Complete)
            {
                FailQuest(CurrentQuest);
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
            CurrentQuest = quest;
            AcceptedQuests.Add(quest);
            CurrentDialogueQuestGiver.RemoveQuest(quest);
            SetQuestStatus(quest, QUEST_STATUS.Accept);
            SetQuestStartTime(quest);
            InitQuestPickUp(quest.QPackage);
            InitQuestDelivery(quest.QPackage);
            InitQuestTimer(quest);
        }

        private void FailQuest(Quest quest)
        {
            if (quest.MaxDeliverTime <= QGUIOverlay.Instance.GetCurrentCount() + 1)
            {
                Debug.Log("Fail quest");
                RoundQuest(quest, QUEST_STATUS.Fail);
            }
        }

        private void CompleteQuest(Quest quest)
        {
            if (quest.QPackage.PackageIsDelivered && quest.QPackage.PackageIsTaken)
            {
                Debug.Log("Complete quest");
                RoundQuest(quest, QUEST_STATUS.Complete);
            }  
        }

        private void RoundQuest(Quest quest, QUEST_STATUS status)
        {
            if (AcceptedQuests.Contains(quest))
            {
                if (CurrentQuest == quest)
                {
                    SetQuestEndTime(quest);
                    SetQuestStatus(quest, status);
                    AcceptedQuests.Remove(quest);
                    QInventory.RemoveQuestPackage(quest.QPackage);

                    if (status == QUEST_STATUS.Complete)
                    {
                        CompletedQuests.Add(quest);
                        QInventory.AddQuestReward(quest.QReward, quest.ScaleReward, quest.ChallengeType);
                    }

                    else if (status == QUEST_STATUS.Fail)
                    {
                        DespawnQuestPrefabs();
                        FailedQuests.Add(quest);
                    }

                    QGUIOverlay.Instance.ShowQuestTimer(false);
                    QGUIOverlay.Instance.ResetTimerValues();
                    ShowQuestCounter = false;
                    CurrentQuest = null;
                    QuestMaxTime = 0;
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
            foreach(GameObject qObject in QuestObjects)
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
            quest.StartTime = Time.time;
        }

        public void SetQuestEndTime(Quest quest)
        {
            quest.EndTime = Time.time;
            quest.TotalTime = quest.EndTime - quest.StartTime;
        }

        public bool IsDialogueSet()
        {
            return CurrentDialogueQuest != null && CurrentDialogueQuestGiver != null;
        }

        public void InitQuestPickUp(QPackage package)
        {
            GameObject pickUp = Instantiate(
                package.PickUpPrefab,
                package.PickUpLocation.Location.position,
                package.PickUpLocation.Location.rotation);

            pickUp.GetComponent<QPickUpDelivery>().SetPackage(package);
            QuestObjects.Add(pickUp);
        }
               
        public void InitQuestDelivery(QPackage package)
        {            
            GameObject delivery = Instantiate(
                package.DeliveryPrefab,
                package.DeliveryLocation.Location.position,
                package.DeliveryLocation.Location.rotation);

            delivery.GetComponent<QPickUpDelivery>().SetPackage(package);
            QuestObjects.Add(delivery);
        }

        public void PickUpPackage(QPackage package)
        {
            package.PackageIsTaken = true;
            QInventory.AddQuestPackage(package);
        }

        // TODO: CompleteQuest should not be called from this method
        public void DeliverPackage(QPackage package)
        {
            package.PackageIsDelivered = true;
            QInventory.RemoveQuestPackage(package);
            CompleteQuest(CurrentQuest);
        }

        public void SetCurrentDiaglogueQuest(Quest quest = null, QGiver qGiver = null)
        {
            CurrentDialogueQuest = quest;
            CurrentDialogueQuestGiver = qGiver;
        }

        public void AddQuestDiscovered(Quest quest)
        {
            if(!DiscoveredQuests.Contains(quest))
            {
                DiscoveredQuests.Add(quest);
            }
        }

    }
}