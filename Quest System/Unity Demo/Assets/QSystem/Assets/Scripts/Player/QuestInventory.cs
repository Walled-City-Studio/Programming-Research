using Boo.Lang;
using System;

namespace QSystem
{
	public class QuestInventory
	{
		private List<Quest> Quests;

		public void AddQuest(Quest quest)
        {
			//popup quest accepted
			Quests.Add(quest);
        }
	}
}
