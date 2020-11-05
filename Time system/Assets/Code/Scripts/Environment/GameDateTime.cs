using System;
using System.Globalization;
using Code.Scripts.Enums;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
{
    [Serializable]
    public class GameDateTime
    {
        // [field: SerializeField]
        // [field: Range(0, (int) TimeUnits.HOURS_IN_DAY)]
        // private int _hour;

        public int Hour
        {
            get { return (int) (ticks / timeSettings.TicksPerHour) % (int) TimeUnits.HOURS_IN_DAY; }
        }

        public int Minute
        {
            get { return (int) (ticks / timeSettings.TicksPerMinute) % (int) TimeUnits.MINUTES_IN_HOUR; }
        }

        public int Second
        {
            get { return (int) (ticks / timeSettings.TicksPerSecond) % (int) TimeUnits.SECONDS_IN_MINUTE; }
        }

        [HideInInspector]
        public uint ticks;

        public TimeSettings timeSettings;

        public GameDateTime(TimeSettings timeSettings, uint ticks)
        {
            this.timeSettings = timeSettings;
            this.ticks = ticks;
        }

        public GameDateTime(TimeSettings timeSettings, GameTime gameTime)
        {
            this.timeSettings = timeSettings;
            Set(gameTime);
        }

        public GameDateTime(TimeSettings timeSettings, int hour = 0, int minute = 0, int second = 0)
        {
            this.timeSettings = timeSettings;
            Set(new GameTime(hour, minute, second));
        }

        // Returns tuple, example usages:
        // 1. var dateTime = dateTime.now();
        //    Debug.Log(dateTime.Item1);
        // 2. var (hour1, minute1, second1)         = dateTime.now();
        // 3. var (_, _, second1)                   = dateTime.now(); (_ are discards)
        // 4. (int hour1, int minute1, int second1) = dateTime.now();
        // public (int, int, int) Now()
        // {
        //     return (Hour, Minute, Second);
        // }

        public GameTime Now()
        {
            return new GameTime(Hour, Minute, Second);
        }
        
        public void Set(GameTime gameTime)
        {
            ticks = GameTimeToTicks(gameTime);
        }

        public void Add(GameTime gameTime)
        {
            ticks += GameTimeToTicks(gameTime);
        }

        public void Subtract(GameTime gameTime)
        {
            ticks -= GameTimeToTicks(gameTime);
        }

        public uint GameTimeToTicks(GameTime gameTime)
        {
            return (uint) (gameTime.hour * timeSettings.TicksPerHour +
                           gameTime.minute * timeSettings.TicksPerMinute +
                           gameTime.second * timeSettings.TicksPerSecond);
        }

        // Compares two instances returns an integer indicating whether
        // the first instance is earlier than, the same as, or later than the second instance.
        public int Compare(GameDateTime gameDateTime1, GameDateTime gameDateTime2)
        {
            if (gameDateTime1.ticks > gameDateTime2.ticks)
            {
                return 1;
            }

            if (gameDateTime1.ticks < gameDateTime2.ticks)
            {
                return -1;
            }

            return 0;
        }

        public int CompareTo(GameDateTime gameDateTime)
        {
            return Compare(this, gameDateTime);
        }

        private DateTime CreateDateTime()
        {
            var dataTime = new DateTime();
            return dataTime
                .AddSeconds(Second)
                .AddMinutes(Minute)
                .AddHours(Hour)
                .AddYears(2100); // TODO: Replace with the year in the game?
        }

        public GameDateTime Copy()
        {
            return new GameDateTime(timeSettings, ticks);
        }

        public override string ToString()
        {
            return CreateDateTime().ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            // TODO: test
            return CreateDateTime().ToString(format, CultureInfo.InvariantCulture);
        }
    }
}