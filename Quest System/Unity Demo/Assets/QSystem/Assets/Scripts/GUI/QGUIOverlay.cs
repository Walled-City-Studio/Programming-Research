using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QSystem
{
    public class QGUIOverlay : Manager<QGUIOverlay>
    {
        public Text QuestMaxTimeText;
        public Text QuestTotalTimeText;

        private float currentCount;

        void Start()
        {
            ShowQuestTimer(false);
        }

        public void ShowQuestTimer(bool show = true)
        {
            gameObject.SetActive(show);
        }

        public void SetQuestTotalTime(float seconds)
        {
            currentCount = seconds;
            QuestTotalTimeText.text = Math.Round(seconds).ToString();
        }

        public void SetQuestMaxTime(float seconds)
        {
            QuestMaxTimeText.text = seconds.ToString(); 
        }

        public void ResetTimerValues()
        {
            QuestMaxTimeText.text = null;
            QuestTotalTimeText.text = null;
            currentCount = 0f;
        }

        public float GetCurrentCount()
        {
            return currentCount;
        }

    }
}
