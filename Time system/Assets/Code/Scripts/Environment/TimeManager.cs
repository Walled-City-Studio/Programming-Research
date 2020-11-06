using System;
using System.Collections;
using Code.Scripts.Enums;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
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

        [SerializeField]
        private TimeSettings timeSettings;

        // Internal representation of the in game time, 1 tick is `timeScale` in game seconds
        // Notes: - Set without timescale
        //        - Use _ticks for the raw amount of ticks
        public float Ticks
        {
            get { return _ticks * TimeScale; }
            private set { _ticks = value; }
        }

        public float RawTicks
        {
            get { return _ticks; }
        }

        // The number of times the in-game time is faster
        public float TimeScale { get; private set; }

        public float RealSecondPerGameMinute { get; private set; }
        
        public float TicksInDay { get; private set; }

        // internal unscaled ticks to keep track of time
        private float _ticks;

        // This is used to regulate event calls, has interconnected relationship with ticks TODO: verify
        private float _ticksElapsed;

        // Helper variable to time the onHour event
        private int _minutesPassed;

        private void Awake()
        {
            // The in game time is `timeScale` times faster than real life
            // 1 real minute = `timeScale` game minutes
            TimeScale = (float) TimeUnits.MINUTES_IN_DAY / timeSettings.realMinutesPerGameDay;

            // Calculates number of real seconds per 1 game minute
            var secondsPerGameDay = timeSettings.realMinutesPerGameDay * (int) TimeUnits.SECONDS_IN_MINUTE;
            RealSecondPerGameMinute = secondsPerGameDay / (float) TimeUnits.MINUTES_IN_DAY;

            // Set initial start time
            Ticks = timeSettings.initialDateTime.ToTicks() / TimeScale;

            // TODO: debug warning if 2 timemanagers or eventmanagers exists?

            #region Debug

            //// var x = new GameDateTime(1,00,00);
            // var x = new GameDateTime(3600);
            // Debug.Log(x.ToTicks());
            // Debug.Log($"{x.hour}:{x.minute}:{x.second}");
            //// var x2 = new GameDateTime(1,33,43);
            // var x2 = new GameDateTime(5623);
            // Debug.Log(x2.ToTicks());
            // Debug.Log($"{x2.hour}:{x2.minute}:{x2.second}");
            //// var x3 = new GameDateTime(0, 1, 00);
            // var x3 = new GameDateTime(60);
            // Debug.Log(x3.ToTicks());
            // Debug.Log($"{x3.hour}:{x3.minute}:{x3.second}");
            //// var x4 = new GameDateTime(0, 0, 30);
            // var x4 = new GameDateTime(30);
            // Debug.Log(x4.ToTicks());
            // Debug.Log($"{x4.hour}:{x4.minute}:{x4.second}");

            // Debug.Log("before set " + Now());
            // Set(5, 5, 5);
            // Debug.Log("set " + Now());

            // Debug.Log("before add " + Now());
            // Add(10, 10, 10);
            // Debug.Log("add " + Now());
            //
            // Debug.Log("before subtract " + Now());
            // Subtract(10, 10, 10);
            // Debug.Log("subtract " + Now());

            /*************************************************************/
            // Debug.Log("timescale " + TimeScale);
            // Debug.Log("_ticks " + _ticks);
            // Debug.Log("Ticks " + Ticks);
            // Debug.Log("now " + Now().hour + "-" + Now().minute + "-" + Now().second);

            // Debug.Log("before set " + Now());
            // Debug.Log("set2 " + Now().hour + "-" + Now().minute + "-" + Now().second);
            //
            // Debug.Log("Hour " + Hour);
            // Debug.Log("Minute " + Minute);
            // Debug.Log("Second " + Second);

            // print("- " + ToTotalHours(457235));
            // print("- " + ToTotalMinutes(457235));
            // print("- " + ToTotalSeconds(457235));
            // ticks 457235 = 5 days 07:00:35
            // ticks 43223  = 12:00:23

            #endregion
        }

        private void Update()
        {
            _ticks += Time.deltaTime;
            _ticksElapsed += Time.deltaTime;
            // Debug.Log($"{_minutesPassed} - T:{_ticks} - T.E:{_ticksElapsed} - D: {Time.deltaTime} - {Hour}:{Minute}:{Second}");

            // If an in game minute has passed.
            if (_ticksElapsed > RealSecondPerGameMinute)
            {
                onMinute?.Invoke(Now());

                // -= instead of _ticksElapsed = 0 because we don't want to discard the decimals and account for overflow.
                // For example when it goes from 0s to 1.99s, we would lose the .99s otherwise.
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

        #region Arithmetic

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

        #region Conversion

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