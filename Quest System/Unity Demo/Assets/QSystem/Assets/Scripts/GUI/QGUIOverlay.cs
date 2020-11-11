using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QSystem
{
    public class QGUIOverlay : Manager<QGUIOverlay>
    {
        public Text questMaxTimeText;
        public Text questTotalTimeText;

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
            questTotalTimeText.text = Math.Round(seconds).ToString();
        }

        public void SetQuestMaxTime(float seconds)
        {
            questMaxTimeText.text = seconds.ToString(); 
        }

        public void ResetTimerValues()
        {
            questMaxTimeText.text = null;
            questTotalTimeText.text = null;
            currentCount = 0f;
        }

        public float GetCurrentCount()
        {
            return currentCount;
        }

    }
}
