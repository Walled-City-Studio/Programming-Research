using System.Globalization;
using Code.Scripts.Environment.TimeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private TimeManager timeManager;

        private Text hour;
        private Text minute;
        private Text second;
        private Text tick;

        private void Start()
        {
            hour = GameObject.Find("Canvas/Hour").GetComponent<Text>();
            minute = GameObject.Find("Canvas/Minute").GetComponent<Text>();
            second = GameObject.Find("Canvas/Second").GetComponent<Text>();
            tick = GameObject.Find("Canvas/Tick").GetComponent<Text>();

            hour.text = timeManager.Hour.ToString();
            minute.text = timeManager.Minute.ToString();
            second.text = timeManager.Second.ToString();
        }

        private void Update()
        {
            second.text = timeManager.Second.ToString();
            tick.text = timeManager.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateHour(GameDateTime gameDateTime)
        {
            hour.text = gameDateTime.hour.ToString();
        }

        private void UpdateMinute(GameDateTime gameDateTime)
        {
            minute.text = gameDateTime.minute.ToString();
        }

        private void OnEnable()
        {
            timeManager.onHour += UpdateHour;
            timeManager.onMinute += UpdateMinute;
        }

        private void OnDisable()
        {
            timeManager.onHour -= UpdateHour;
            timeManager.onMinute -= UpdateMinute;
        }
    }
}