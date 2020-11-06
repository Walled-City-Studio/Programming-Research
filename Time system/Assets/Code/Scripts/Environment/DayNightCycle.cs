using System;
using Code.Scripts.Enums;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
{
    [ExecuteAlways]
    public class DayNightCycle : MonoBehaviour
    {
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

            RenderSettings.ambientLight = dayNightSettings.ambientColor.Evaluate(_timeOfDay);
            RenderSettings.fogColor = dayNightSettings.fogColor.Evaluate(_timeOfDay);
            sunLight.transform.localRotation = Quaternion.Euler(new Vector3((_timeOfDay * 360f) - 180f, 180, 0));
        }
    }
}