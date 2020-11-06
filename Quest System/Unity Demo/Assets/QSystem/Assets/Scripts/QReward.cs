using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "QuestReward", menuName = "Quest/QuestReward", order = 1)]
    public class QReward : ScriptableObject
    {
        [SerializeField] private REWARD_TYPE RewardType;
        
        [SerializeField] private RESOURCE_TYPE ResourceType;

        [SerializeField] private GameObject Item;

        [SerializeField] private int Ammount;

        [SerializeField] private float MaxTimeConsumption;

        [SerializeField] private float MaxRewardAmountTimeScale;

        private float RewardScale;

        public QReward(float MaxQuestTime)
        {
            MaxRewardAmountTimeScale = MaxQuestTime;
        }

        public void StopTimerAt(float CompletionTime)
        {
            RewardScale = (MaxTimeConsumption / CompletionTime) * 100.0f;
        }

        public List<GameObject> GetRewardObject()
        {
            if (RewardScale <= MaxRewardAmountTimeScale)
            {
                List<GameObject> RewardList = new List<GameObject>();
                Ammount *= (int)(RewardScale);
                for(int i = 0; i != Ammount; i++)
                {
                    RewardList.Add(Item);
                }
                return RewardList;
            }
            else
            {
                return null;
            }
        }
    }
}
