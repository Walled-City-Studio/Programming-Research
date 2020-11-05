using System;
using Code.Scripts.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Environment
{
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
            var timePassed = (float)(timeManager.Ticks % timeSettings.TicksPerDay) / timeSettings.TicksPerDay;

            RenderSettings.ambientLight = dayNightSettings.ambientColor.Evaluate(timePassed);
            RenderSettings.fogColor = dayNightSettings.fogColor.Evaluate(timePassed);
            sunLight.transform.localRotation = Quaternion.Euler(new Vector3((timePassed * 360f) - 90f, 180, 0));
        }
    }
}