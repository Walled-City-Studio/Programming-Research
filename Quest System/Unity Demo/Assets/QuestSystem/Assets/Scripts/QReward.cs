using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QReward : MonoBehaviour
    {
        [SerializeField] private GameObject RewardItem;
        [SerializeField] private float BaseRewardMoney;
        [SerializeField] private float MaxTimeConsumption;
        [SerializeField] private float MaxRewardAmountTimeScale;
        private float RewardScale;

        public QReward(float MaxQuestTime)
        {
            MaxRewardAmountTimeScale = MaxQuestTime;
        }

        public void StopTimerAt(float CompletionTime)
        {
            RewardScale = MaxTimeConsumption / CompletionTime;
        }  
        
        public GameObject GetRewardObject()
        {
            if (RewardScale <= 1.0f)
            {
                return RewardItem;
            }
            else
            {
                return null;
            }
        }

        public float GetRewardMoney()
        {
            if(RewardScale <= MaxRewardAmountTimeScale)
            {
                return BaseRewardMoney * RewardScale;
            }
            else
            {
                return 0.0f;
            }
        }
    }
}