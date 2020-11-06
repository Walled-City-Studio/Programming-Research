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

    }
}
