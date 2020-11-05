using System;

namespace Code.Scripts.Environment
{
    public struct ScheduledEvent
    {
        public string EventName;
        public GameTime GameTime;
        public Action Callback;

        public ScheduledEvent(string eventName, Action callback, int hour = 0, int minute = 0, int second = 0)
        {
            EventName = eventName;
            GameTime = new GameTime(hour, minute, second);
            Callback = callback;
        }

        public ScheduledEvent(string eventName, GameTime gameTime, Action callback)
        {
            EventName = eventName;
            GameTime = gameTime;
            Callback = callback;
        }
    }
}