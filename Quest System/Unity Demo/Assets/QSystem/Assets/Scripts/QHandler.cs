using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class QHandler : Manager<QHandler>
    {
        private QInventory QInventory;

        private List<Quest> AcceptedQuests = new List<Quest>();
        private List<Quest> CompletedQuests = new List<Quest>();
        private List<Quest> DiscoveredQuests = new List<Quest>();

        private Quest CurrentQuest;
        private Quest CurrentDialogueQuest;

        private QGiver CurrentDialogueQuestGiver;

        void Start()
        {
            QInventory = FindObjectOfType<PlayerController>().QInventory;
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

        public void AcceptQuest(Quest quest)
        {
            CurrentQuest = quest;
            AcceptedQuests.Add(quest);
            CurrentDialogueQuestGiver.RemoveQuest(quest);
            SetQuestStatus(quest, QUEST_STATUS.Accept);
            SetQuestStartTime(quest);
            InitQuestPickUp(quest.QPackage);
            InitQuestDelivery(quest.QPackage);
        }

        private void CompleteQuest(Quest quest)
        {
            if (AcceptedQuests.Contains(quest))
            {
                if (CurrentQuest == quest)
                {
                    if (quest.QPackage.PackageIsDelivered && quest.QPackage.PackageIsTaken)
                    {
                        SetQuestEndTime(quest);
                        SetQuestTotalTime(quest);
                        AcceptedQuests.Remove(quest);
                        CompletedQuests.Add(quest);
                        QInventory.AddQuestReward(quest.QReward, quest.ScaleRewardChallenge, quest.ChallengeType);
                    }
                    else
                    {
                        Debug.Log("The package isn't picked and/or delivered.");
                    }
                }
                else
                {
                    Debug.Log("It's not allowed to complete other quests then the current quest.");
                }
            }
            else
            {
                Debug.Log("It's not allowed to complete quests that aren't accepted.");
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
        }

        public bool IsDialogueSet()
        {
            return CurrentDialogueQuest != null && CurrentDialogueQuestGiver != null;
        }

        public void SetQuestTotalTime(Quest quest)
        {
            if (quest.StartTime != 0.0f || quest.EndTime != 0.0f)
            {
                quest.TotalTime = quest.EndTime - quest.StartTime;
            }
            else
            {
                Debug.Log("Cant calculate quest.TotalTime, quest.StartTime or quest.Endtime isn't set.");
            }
        }


        // TODO: Maybe refactor InitQuestPickUp and InitQuestDelivery to a single method (DRY but less readable)
        public void InitQuestPickUp(QPackage package)
        {
            GameObject pickUp = Instantiate(
                package.PickUpPrefab,
                package.PickUpLocation.Location.position,
                package.PickUpLocation.Location.rotation);

            SetPackage(pickUp.GetComponent<QPickUpDelivery>(), package);
        }

        // TODO: Maybe refactor InitQuestPickUp and InitQuestDelivery to a single method (DRY but less readable)
        public void InitQuestDelivery(QPackage package)
        {
            // TODO: Instead of DeliveryPrefab it can be DeliveryTransform. Add additional Transform logics. See: QPackage & QPackageEditor
            GameObject delivery = Instantiate(
                package.DeliveryPrefab,
                package.DeliveryLocation.Location.position,
                package.DeliveryLocation.Location.rotation);

            SetPackage(delivery.GetComponent<QPickUpDelivery>(), package);
        }

        public void SetPackage(QPickUpDelivery script, QPackage qPackage)
        {
            if (script != null)
            {
                script.SetPackage(qPackage);
            }
            else
            {
                Debug.Log("Package PickUp prefab doens't have 'QPickUpDelivery' script");
            }
        }

        public void PickUpPackage(QPackage package)
        {
            Debug.Log("asd");
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