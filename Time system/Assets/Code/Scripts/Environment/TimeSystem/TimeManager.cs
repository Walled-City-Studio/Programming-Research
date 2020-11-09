using System;
using Code.Scripts.Enums;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment.TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        #region Events

        public event Action<GameDateTime> onHour;
        public event Action<GameDateTime> onMinute;

        #endregion

        #region Time Properties

        public int Hour
        {
            get { return ToHour((int) Ticks); }
        }

        public int Minute
        {
            get { return ToMinute((int) Ticks); }
        }

        public int Second
        {
            get { return ToSecond((int) Ticks); }
        }

        #endregion

        // Internal representation of the in game time, 1 tick is `timeScale` in game seconds
        // Note: set without applying timescale
        public float Ticks
        {
            get { return _ticks * TimeScale; }
        }

        public float RawTicks
        {
            get { return _ticks; }
        }

        [field: SerializeField]
        private TimeSettings settings;

        public TimeSettings Settings
        {
            get { return settings; }
        }

        // The number of times the in game time is faster
        public float TimeScale { get; private set; }

        public float RealSecondPerGameMinute { get; private set; }

        // Internal unscaled ticks to keep track of time, equivalent to seconds
        private float _ticks;

        // This is used to regulate event calls, has interconnected relationship with _ticks
        private float _ticksElapsed;

        // Helper variable to time the onHour event
        private int _minutesPassed;

        private void Awake()
        {
            if (settings == null)
            {
                Debug.LogError($"{nameof(TimeManager)} requires an instance of {nameof(TimeSettings)}.");
                return;
            }

            // The in game time is `timeScale` times faster
            // 1 real minute = `timeScale` game minutes
            TimeScale = (float) TimeUnits.MINUTES_IN_DAY / settings.realMinutesPerGameDay;

            // Calculates number of real seconds per 1 game minute
            int secondsPerGameDay = settings.realMinutesPerGameDay * (int) TimeUnits.SECONDS_IN_MINUTE;
            RealSecondPerGameMinute = secondsPerGameDay / (float) TimeUnits.MINUTES_IN_DAY;

            // Set initial start time
            _ticks = settings.initialDateTime.ToTicks() / TimeScale;
            _minutesPassed = settings.initialDateTime.minute;
        }

        private void Update()
        {
            _ticks += Time.deltaTime;
            _ticksElapsed += Time.deltaTime;
            
            // If an in game minute has passed
            if (_ticksElapsed > RealSecondPerGameMinute)
            {
                onMinute?.Invoke(Now());

                // -= instead of _ticksElapsed = 0 because we want to account for overflow and don't discard the decimals
                // For example when it goes from 0s to 1.99s, we would lose the .99s
                _ticksElapsed -= RealSecondPerGameMinute;
                _minutesPassed++;

                if (_minutesPassed == (int) TimeUnits.MINUTES_IN_HOUR)
                {
                    _minutesPassed = 0;
                    onHour?.Invoke(Now());
                }
            }
        }

        public GameDateTime Now()
        {
            return new GameDateTime(Hour, Minute, Second);
        }

        // Returns tuple, example usages:
        // 1. var dateTime = dateTime.now();
        //    Debug.Log(dateTime.Item1);
        // 2. var (hour1, minute1, second1)         = dateTime.now();
        // 3. var (_, _, second1)                   = dateTime.now(); (_ are discards)
        // 4. (int hour1, int minute1, int second1) = dateTime.now();
        public (int, int, int) NowTuple()
        {
            return (Hour, Minute, Second);
        }

        #region Arithmetic and overloads

        public void Set(GameDateTime gameDateTime)
        {
            _ticks = gameDateTime.ToTicks() / TimeScale;
        }

        public void Set(int hour, int minute, int second)
        {
            Set(new GameDateTime(hour, minute, second));
        }

        public void Add(GameDateTime gameDateTime)
        {
            _ticks += gameDateTime.ToTicks() / TimeScale;
        }

        public void Add(int hour, int minute, int second)
        {
            Add(new GameDateTime(hour, minute, second));
        }

        public void Subtract(GameDateTime gameDateTime)
        {
            _ticks -= gameDateTime.ToTicks() / TimeScale;
        }

        public void Subtract(int hour, int minute, int second)
        {
            Subtract(new GameDateTime(hour, minute, second));
        }

        #endregion

        #region Conversion and overloads

        public static int ToHour(int ticks)
        {
            return ToTotalHours(ticks) % (int) TimeUnits.HOURS_IN_DAY;
        }

        public static int ToMinute(int ticks)
        {
            return ToTotalMinutes(ticks) % (int) TimeUnits.MINUTES_IN_HOUR;
        }

        public static int ToSecond(int ticks)
        {
            return ToTotalSeconds(ticks) % (int) TimeUnits.SECONDS_IN_MINUTE;
        }

        public static int ToTotalHours(float tick)
        {
            return (int) (tick / (int) TimeUnits.SECONDS_IN_HOUR);
        }

        public static int ToTotalMinutes(float tick)
        {
            return (int) (tick / (int) TimeUnits.SECONDS_IN_MINUTE);
        }

        public static int ToTotalSeconds(float tick)
        {
            return (int) tick;
        }

        #endregion
    }
}