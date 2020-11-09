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


        /*[SerializeField] public float MaxTimeConsumption;

       private float MaxQuestTime;

       private float RewardScale;

       public void SetQuestTime(float maxQuestTime)
       {
           MaxQuestTime = maxQuestTime;
       }

       public void StopTimerAt(float CompletionTime)
       {
           RewardScale = (MaxTimeConsumption / CompletionTime) * 100.0f;
       }

       public List<GameObject> GetRewardObject()
       {
           if (RewardScale <= MaxQuestTime)
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
       }*/
    }
}
