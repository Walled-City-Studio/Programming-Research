using Code.Scripts.Environment.TimeSystem;
using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DayNightPreset", menuName = "Time/DayNightPreset", order = 1)]
    public class DayNightSettings : ScriptableObject
    {
        // Sunrise, max sun intensity, sunset, dark
        public GameDateTime dawn, noon, dusk, night;
        public Gradient ambientColor;
    }
}