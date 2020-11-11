using System;
using Code.Scripts.Environment.TimeSystem;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TimeSettings", menuName = "Time/TimeSettings", order = 0)]
    public class TimeSettings : ScriptableObject
    {
        public GameDateTime initialDateTime = new GameDateTime(12, 0, 0);
        public int realMinutesPerGameDay = 30;
    }
}