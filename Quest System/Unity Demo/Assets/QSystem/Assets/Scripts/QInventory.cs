﻿using Boo.Lang;
using System;
using System.Linq;
using UnityEngine;

namespace QSystem
{
	public class QInventory
	{
		private float[] QuestResourceRewards = new float[Enum.GetNames(typeof(RESOURCE_TYPE)).Length];

		private List<GameObject> QuestItemRewards = new List<GameObject>();

		private List<QPackage> QPackages = new List<QPackage>();

		public void AddQuestPackage(QPackage qPackage)
		{
			QPackages.Add(qPackage);
		}

		public void RemoveQuestPackage(QPackage qPackage)
        {
			QPackages.Remove(qPackage);
		}
		
		// TODO: Quest reward calculation shouldn't be done in QInventory
		public void AddQuestReward(QReward qReward, bool scale, CHALLENGE_TYPE factor)
        {
			int ammount = scale ? ScaleAmmount(qReward.Ammount, (int)factor) : qReward.Ammount;
			if (qReward.RewardType == REWARD_TYPE.Item)
            {
				QuestItemRewards.AddRange(Enumerable.Repeat(qReward.Item, ammount));
			}
			else if(qReward.RewardType == REWARD_TYPE.Resource)
            {
				QuestResourceRewards[(int)qReward.ResourceType] += ammount;
			}
        }

		// TODO: Quest reward calculation shouldn't be done in QInventory
		public int ScaleAmmount(int ammount, int factor)
        {
			return ammount + (factor * ammount);
		}

		public void RemoveQuestResource(RESOURCE_TYPE resourceType, int ammount)
		{
			QuestResourceRewards[(int)resourceType] += ammount;
		}

		public void RemoveQuestItem(QPackage qPackage)
		{
			QPackages.Remove(qPackage);
		}

	}
}
