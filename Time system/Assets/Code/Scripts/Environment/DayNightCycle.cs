using System;
using Code.Scripts.Enums;
using Code.Scripts.Environment.TimeSystem;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
{
    [ExecuteAlways]
    public class DayNightCycle : MonoBehaviour
    {
        private const float SunInitialIntensity = 1;

        [SerializeField]
        private TimeSettings timeSettings;

        [SerializeField]
        private TimeManager timeManager;

        [SerializeField]
        private Light sunLight;

        [SerializeField]
        private DayNightSettings dayNightSettings;

        [SerializeField]
        [Range(0, 24)]
        private float timeOfDay;

        // Internal use
        private float _timeOfDay;

        private float _intensityMultiplier = .8f;

        private void Start()
        {
            if (sunLight != null && RenderSettings.sun != null)
            {
                sunLight = RenderSettings.sun;

                if (sunLight == null)
                {
                    Debug.LogError("Expected a minimum of 1 (directional) light in the scene");
                }
            }
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                _timeOfDay = (timeManager.Ticks / (int) TimeUnits.SECONDS_IN_DAY) % 1;
            }
            else
            {
                _timeOfDay = timeOfDay / (float) TimeUnits.HOURS_IN_DAY;
            }

            if (_timeOfDay <= Hour(5.52f) || _timeOfDay >= Hour(18))
            {
                _intensityMultiplier = 0;
            }
            else if (_timeOfDay <= Hour(6f))
            {
                _intensityMultiplier = Mathf.Clamp01((_timeOfDay - Hour(5.52f)) * (1 / 0.02f));
            }
            else if (_timeOfDay >= Hour(17.5f))
            {
                _intensityMultiplier = Mathf.Clamp01(1 - ((_timeOfDay - Hour(17.5f)) * (1 / 0.02f)));
            }

            sunLight.intensity = SunInitialIntensity * _intensityMultiplier;

            RenderSettings.ambientLight = dayNightSettings.ambientColor.Evaluate(_timeOfDay);
            RenderSettings.fogColor = dayNightSettings.fogColor.Evaluate(_timeOfDay);
            sunLight.transform.localRotation = Quaternion.Euler((_timeOfDay * 360f) - 90, 170, 0);
        }

        private float Hour(float hour)
        {
            return hour / (int) TimeUnits.HOURS_IN_DAY;
        }

        private bool BetweenHours(int from, int to)
        {
            float time = _timeOfDay * (int) TimeUnits.HOURS_IN_DAY;
            return time >= from && time < to;
        }
    }
}