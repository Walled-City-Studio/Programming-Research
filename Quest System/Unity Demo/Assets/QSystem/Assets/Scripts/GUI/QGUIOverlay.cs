using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QSystem
{
    public class QGUIOverlay : GUIManager<QGUIOverlay>
    {
        public Text QuestMaxTimeText;
        public Text QuestTotalTimeText;

        private GameObject QuestTimer;

        void Start()
        {
            QuestTimer = gameObject.transform.GetChild(0).gameObject;
        }

        public void setQuestTimer(Time t)
        {
            //QuestTimer.SetActive(show);
            //QuestMaxTimeText.text = t.time;
        }

        public void showQuestTimer(bool show = true)
        {
            QuestTimer.SetActive(show);
        }

    }
}
