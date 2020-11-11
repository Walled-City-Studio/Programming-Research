using System;
using System.Globalization;
using Code.Scripts.Enums;
using UnityEngine;

namespace Code.Scripts.Environment.TimeSystem
{
    [Serializable]
    public class GameDateTime
    {
        [Range(0, 24)]
        public int hour;

        [Range(0, 60)]
        public int minute;

        [Range(0, 60)]
        public int second;

        public GameDateTime(int hour, int minute = 0, int second = 0)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public GameDateTime(int ticks)
        {
            hour = TimeManager.ToHour(ticks);
            minute = TimeManager.ToMinute(ticks);
            second = TimeManager.ToSecond(ticks);
        }

        public void Add(int hour, int minute = 0, int second = 0)
        {
            this.hour += hour;
            this.minute += minute;
            this.second += second;
        }

        public void Subtract(int hour, int minute = 0, int second = 0)
        {
            this.hour -= hour;
            this.minute -= minute;
            this.second -= second;
        }

        public int ToTicks()
        {
            return second +
                   minute * (int) TimeUnits.SECONDS_IN_MINUTE +
                   hour * (int) TimeUnits.SECONDS_IN_HOUR;
        }

        // Compares two instances returns an integer indicating whether
        // the first instance is earlier than, the same as, or later than the second instance.
        public int Compare(GameDateTime gameDateTime1, GameDateTime gameDateTime2)
        {
            // Instance 1 is later than 2
            if (gameDateTime1.ToTicks() > gameDateTime2.ToTicks())
            {
                return 1;
            }

            // Instance 1 is earlier than 2
            if (gameDateTime1.ToTicks() < gameDateTime2.ToTicks())
            {
                return -1;
            }

            // Instance 1 and 2 are at the same moment
            return 0;
        }

        public int CompareTo(GameDateTime gameDateTime)
        {
            return Compare(this, gameDateTime);
        }

        public GameDateTime Copy()
        {
            return new GameDateTime(ToTicks());
        }

        public override string ToString()
        {
            return CreateDateTime().ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            return CreateDateTime().ToString(format, CultureInfo.InvariantCulture);
        }

        private DateTime CreateDateTime()
        {
            return new DateTime()
                .AddSeconds(second)
                .AddMinutes(minute)
                .AddHours(hour);
        }
    }
}