using System;
using Code.Scripts.Enums;
using Code.Scripts.Environment;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TimeSettings", menuName = "Time/TimeSettings", order = 0)]
    public class TimeSettings : ScriptableObject
    {
        public GameTime initialTime = new GameTime(12); // TODO: test
        public int realMinutesPerGameDay = 20;
        public int ticksPerRealSecond = 20;
        public float timeScale = 1;

        public int TicksPerDay
        {
            get
            {
                return ticksPerRealSecond *
                       (int) TimeUnits.SECONDS_IN_MINUTE *
                       realMinutesPerGameDay;
            }
        }

        public int TicksPerHour
        {
            get { return TicksPerDay / (int) TimeUnits.HOURS_IN_DAY; }
        }

        public int TicksPerMinute
        {
            get { return TicksPerDay / (int) TimeUnits.MINUTES_IN_HOUR; }
        }

        public int TicksPerSecond
        {
            get { return TicksPerDay / (int) TimeUnits.SECONDS_IN_MINUTE; }
        }
    }
}