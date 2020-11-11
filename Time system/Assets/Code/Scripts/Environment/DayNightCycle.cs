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
        private const float SUN_ROTATION = 360;
        private const int OFFSET = 90;

        [SerializeField]
        private TimeManager timeManager;

        [SerializeField]
        private DayNightSettings cycle;

        [SerializeField]
        private Light sunLight;

        [SerializeField]
        [Header("Edit mode only")]
        [Tooltip("Change the start time in the TimeSettings scriptable object.")]
        [Range(0, 24)]
        private float timeOfDay;

        // Internal use
        private float timeOfDayTicks;

        private float intensityMultiplier;

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
            UpdateTimeOfDay();
            UpdateLighting();

            RenderSettings.ambientLight = cycle.ambientColor.Evaluate(TicksToPercent(timeOfDayTicks));
            sunLight.transform.localRotation =
                Quaternion.Euler((TicksToPercent(timeOfDayTicks) * SUN_ROTATION) - OFFSET, 0, 0);
        }

        private void UpdateTimeOfDay()
        {
            if (Application.isPlaying)
            {
                timeOfDayTicks = timeManager.Ticks % (int) TimeUnits.SECONDS_IN_DAY;

                // Update value in inspector
                timeOfDay = timeOfDayTicks / (float) TimeUnits.SECONDS_IN_DAY * (int) TimeUnits.HOURS_IN_DAY;
            }
            else // Edit mode
            {
                timeOfDayTicks = timeOfDay * (float) TimeUnits.SECONDS_IN_DAY / (int) TimeUnits.HOURS_IN_DAY;
            }
        }

        private void UpdateLighting()
        {
            // Sunrise
            if (Between(cycle.dawn, cycle.noon))
            {
                // Ramp up intensity from 0 to 1
                intensityMultiplier =
                    Mathf.Clamp01(1 - (cycle.dawn.ToTicks() - timeOfDayTicks) / cycle.dawn.ToTicks());
            }
            // Sunset
            else if (Between(cycle.dusk, cycle.night))
            {
                // Ramp down intensity from 1 to 0
                intensityMultiplier =
                    Mathf.Clamp01((timeOfDayTicks - cycle.dusk.ToTicks()));
            }

            sunLight.intensity = intensityMultiplier;
        }

        private bool Before(GameDateTime time)
        {
            return timeOfDayTicks < time.ToTicks();
        }

        private bool After(GameDateTime time)
        {
            return timeOfDayTicks >= time.ToTicks();
        }

        private bool Between(GameDateTime time1, GameDateTime time2)
        {
            return After(time1) && Before(time2);
        }

        private float TicksToPercent(float ticks)
        {
            return ticks / (int) TimeUnits.SECONDS_IN_DAY % 1;
        }
    }
}