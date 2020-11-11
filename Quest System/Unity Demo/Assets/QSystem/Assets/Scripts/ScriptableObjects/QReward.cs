using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestReward", menuName = "Quest/Quest Reward", order = 3)]
    public class QReward : ScriptableObject
    {
        [SerializeField] 
        public REWARD_TYPE rewardType;
        
        [SerializeField] 
        public RESOURCE_TYPE resourceType;

        [SerializeField] 
        public GameObject item;

        [SerializeField] 
        public int ammount;
                
    }
}
