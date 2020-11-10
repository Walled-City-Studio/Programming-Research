using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    [CreateAssetMenu(fileName = "QuestReward", menuName = "Quest/Quest Reward", order = 3)]
    public class QReward : ScriptableObject
    {
        [SerializeField] public REWARD_TYPE RewardType;
        
        [SerializeField] public RESOURCE_TYPE ResourceType;

        [SerializeField] public GameObject Item;

        [SerializeField] public int Ammount;
                
    }
}
