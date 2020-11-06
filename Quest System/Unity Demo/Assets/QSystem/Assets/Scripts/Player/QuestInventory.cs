﻿using Boo.Lang;
using System;

namespace QSystem
{
	public class QuestInventory
	{
		private int InventorySize;
		private int CurrentOccupiedSpace = 0;
		private List<Quest> Quests;

		public QuestInventory(int Size)
        {
			InventorySize = Size;
        }

		public bool AddQuest(Quest quest)
        {
            if (CurrentOccupiedSpace + (int)quest.QPackage.Size <= InventorySize)
            {
                Quests.Add(quest);
                //popup quest accepted
                return true;
            }
            else
            {
                //popup to many packages
                return false;
            }
            
        }
	}
}
