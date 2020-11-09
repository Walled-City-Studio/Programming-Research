using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DayNightPreset", menuName = "Time/DayNightPreset", order = 1)]
    public class DayNightSettings : ScriptableObject
    {
        public Gradient ambientColor;
        public Gradient fogColor;
        public Gradient lightColor;
    }
}