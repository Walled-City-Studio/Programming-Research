using System;
using System.Collections;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
{
    public class TimeManager : MonoBehaviour
    {
        public static event Action<int, int, int> onGameTick;
        public static event Action<int> onHour;
        public static event Action<int> onMinute;
        public static event Action<int> onSecond;

        [SerializeField]
        private TimeSettings timeSettings;

        private float tickRate;

        private GameDateTime _gameDateTime;

        public GameDateTime gameDateTime
        {
            get { return gameDateTime.Copy(); }
            private set { _gameDateTime = value; }
        }

        public uint Ticks
        {
            get { return _gameDateTime.ticks; }
        }

        private void Start()
        {
            tickRate = 1f / (timeSettings.ticksPerRealSecond * timeSettings.timeScale);

            var start = timeSettings.initialTime;
            gameDateTime = new GameDateTime(timeSettings, start.hour, start.minute, start.second);
            StartCoroutine(nameof(Tick));

            // debug warning if 2 timemanagers or eventmanagers exists?
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                var ticks = _gameDateTime.ticks++;
                onGameTick?.Invoke(_gameDateTime.Hour, _gameDateTime.Minute, _gameDateTime.Second);

                if (ticks % timeSettings.TicksPerHour == 0)
                {
                    onHour?.Invoke(_gameDateTime.Hour);
                }

                if (ticks % timeSettings.TicksPerMinute == 0)
                {
                    onMinute?.Invoke(_gameDateTime.Minute);
                }

                if (ticks % timeSettings.TicksPerSecond == 0)
                {
                    onSecond?.Invoke(_gameDateTime.Second);
                }

                yield return new WaitForSecondsRealtime(tickRate);
            }
        }
        
        private void OnDisable()
        {
            StopTicking();
        }

        private void OnDestroy()
        {
            StopTicking();
        }

        public void StopTicking()
        {
            StopCoroutine(nameof(Tick)); 
        }
    }
}