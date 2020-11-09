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
        private float internalTimeOfDay;

        private float intensityMultiplier = .8f;

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
                internalTimeOfDay = (timeManager.Ticks / (int) TimeUnits.SECONDS_IN_DAY) % 1;
            }
            else
            {
                internalTimeOfDay = timeOfDay / (float) TimeUnits.HOURS_IN_DAY;
            }

            if (internalTimeOfDay <= Hour(5.52f) || internalTimeOfDay >= Hour(18))
            {
                intensityMultiplier = 0;
            }
            else if (internalTimeOfDay <= Hour(6f))
            {
                intensityMultiplier = Mathf.Clamp01((internalTimeOfDay - Hour(5.52f)) * (1 / 0.02f));
            }
            else if (internalTimeOfDay >= Hour(17.5f))
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((internalTimeOfDay - Hour(17.5f)) * (1 / 0.02f)));
            }

            sunLight.intensity = SunInitialIntensity * intensityMultiplier;

            RenderSettings.ambientLight = dayNightSettings.ambientColor.Evaluate(internalTimeOfDay);
            RenderSettings.fogColor = dayNightSettings.fogColor.Evaluate(internalTimeOfDay);
            sunLight.transform.localRotation = Quaternion.Euler((internalTimeOfDay * 360f) - 90, 170, 0);
        }

        private float Hour(float hour)
        {
            return hour / (int) TimeUnits.HOURS_IN_DAY;
        }

        private bool BetweenHours(int from, int to)
        {
            float time = internalTimeOfDay * (int) TimeUnits.HOURS_IN_DAY;
            return time >= from && time < to;
        }
    }
}