using System;
using System.Globalization;
using Code.Scripts.Environment;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private TimeManager timeManager;

        private Text _hour;
        private Text _minute;
        private Text _second;
        private Text _tick;

        private void Start()
        {
            _hour = GameObject.Find("Canvas/Hour").GetComponent<Text>();
            _minute = GameObject.Find("Canvas/Minute").GetComponent<Text>();
            _second = GameObject.Find("Canvas/Second").GetComponent<Text>();
            _tick = GameObject.Find("Canvas/Tick").GetComponent<Text>();

            _hour.text = timeManager.Hour.ToString();
            _minute.text = timeManager.Minute.ToString();
            _second.text = timeManager.Second.ToString();
        }

        private void Update()
        {
            _second.text = timeManager.Second.ToString();
            _tick.text = timeManager.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateHour(GameDateTime gameDateTime)
        {
            _hour.text = gameDateTime.hour.ToString();
        }

        private void UpdateMinute(GameDateTime gameDateTime)
        {
            _minute.text = gameDateTime.minute.ToString();
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