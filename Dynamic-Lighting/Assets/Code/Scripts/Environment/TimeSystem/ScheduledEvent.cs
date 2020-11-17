using System;

namespace Code.Scripts.Environment.TimeSystem
{
    public struct ScheduledEvent
    {
        public Action<GameDateTime> Callback;
        public GameDateTime GameDateTime;
        public string EventName;

        public ScheduledEvent(Action<GameDateTime> callback, int hour = 0, int minute = 0, int second = 0,
            string eventName = null)
        {
            Callback = callback;
            GameDateTime = new GameDateTime(hour, minute, second);
            EventName = eventName;
        }

        public ScheduledEvent(Action<GameDateTime> callback, GameDateTime gameDateTime, string eventName = null)
        {
            Callback = callback;
            GameDateTime = gameDateTime;
            EventName = eventName;
        }

        public override string ToString()
        {
            return $"Event {EventName ?? "<without_name>"} scheduled for {GameDateTime.ToString("HH:mm:ss")}";
        }
    }
}