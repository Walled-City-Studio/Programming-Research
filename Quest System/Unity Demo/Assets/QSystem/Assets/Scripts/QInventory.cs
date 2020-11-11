using Boo.Lang;
using System;
using System.Linq;
using UnityEngine;

namespace QSystem
{
	public class QInventory
	{
		// RESOURCE_TYPE index acuals QuestResourceReward type
		private float[] questResourceRewards = new float[Enum.GetNames(typeof(RESOURCE_TYPE)).Length];

		private List<GameObject> questItemRewards = new List<GameObject>();

		private List<QPackage> qPackages = new List<QPackage>();

		public void AddQuestPackage(QPackage qPackage)
		{
			qPackages.Add(qPackage);
		}

		public void RemoveQuestPackage(QPackage qPackage)
        {
			qPackages.Remove(qPackage);
		}
		
		// TODO: Quest reward calculation shouldn't be done in QInventory
		public void AddQuestReward(QReward qReward, bool scale = false, CHALLENGE_TYPE factor = CHALLENGE_TYPE.EASY)
        {
			int amount = scale ? ScaleAmount(qReward.amount, (int)factor) : qReward.amount;
			if (qReward.rewardType == REWARD_TYPE.ITEM)
            {
				Debug.Log("Add quest item: " + qReward.item.name + ", " + amount + " times.");
				questItemRewards.AddRange(Enumerable.Repeat(qReward.item, amount));
			}
			else if(qReward.rewardType == REWARD_TYPE.RESOURCE)
            {
				Debug.Log("Add quest resource: " + qReward.resourceType.ToString() + ", " + amount + " times.");
				questResourceRewards[(int)qReward.resourceType] += amount;
			}
        }

		// TODO: Quest reward calculation shouldn't be done in QInventory
		public int ScaleAmount(int amount, int factor)
        {
			return amount + (factor * amount);
		}

		public void RemoveQuestResource(RESOURCE_TYPE resourceType, int amount)
		{
			questResourceRewards[(int)resourceType] += amount;
		}

		public void RemoveQuestItem(QPackage qPackage)
		{
			qPackages.Remove(qPackage);
		}

	}
}
