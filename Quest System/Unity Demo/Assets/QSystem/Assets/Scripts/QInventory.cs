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
			int ammount = scale ? ScaleAmmount(qReward.ammount, (int)factor) : qReward.ammount;
			if (qReward.rewardType == REWARD_TYPE.ITEM)
            {
				Debug.Log("Add quest item: " + qReward.item.name + ", " + ammount + " times.");
				questItemRewards.AddRange(Enumerable.Repeat(qReward.item, ammount));
			}
			else if(qReward.rewardType == REWARD_TYPE.RESOURCE)
            {
				Debug.Log("Add quest resource: " + qReward.resourceType.ToString() + ", " + ammount + " times.");
				questResourceRewards[(int)qReward.resourceType] += ammount;
			}
        }

		// TODO: Quest reward calculation shouldn't be done in QInventory
		public int ScaleAmmount(int ammount, int factor)
        {
			return ammount + (factor * ammount);
		}

		public void RemoveQuestResource(RESOURCE_TYPE resourceType, int ammount)
		{
			questResourceRewards[(int)resourceType] += ammount;
		}

		public void RemoveQuestItem(QPackage qPackage)
		{
			qPackages.Remove(qPackage);
		}

	}
}
