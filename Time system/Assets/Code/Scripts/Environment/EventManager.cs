using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Environment
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField]
        private TimeManager timeManager; // TODO: Maybe force 1 instance and get it via code?

        private SortedList<uint, ScheduledEvent> scheduledEvents;

        private void Start()
        {
            scheduledEvents = new SortedList<uint, ScheduledEvent>(new TickComparer());
        }

        private void Update()
        {
            CallEvent(timeManager.Ticks);
        }

        public void CallEvent(uint ticks)
        {
            while (!scheduledEvents.Any())
            {
                var scheduledEvent = scheduledEvents.First();
                var eventTicks = ConvertGameTimeToTicks(scheduledEvent.Value.GameTime);
                if (ticks < eventTicks)
                {
                    return;
                }

                if (scheduledEvents.TryGetValue(scheduledEvent.Key, out var @event))
                {
                    @event.Callback();
                    scheduledEvents.Remove(scheduledEvent.Key);

                    Debug.Log($"Executed event: {@event.EventName} at {@event.GameTime.ToString()}");
                }
            }
        }

        public void AddEvent(ScheduledEvent scheduledEvent)
        {
            var ticks = ConvertGameTimeToTicks(scheduledEvent.GameTime);

            if (ticks > timeManager.Ticks)
            {
                Debug.LogWarning("Can't schedule an event with a time in the past.");
                return;
            }

            if (scheduledEvents.Any(@event => scheduledEvent.EventName.Equals(@event.Value.EventName)))
            {
                Debug.LogWarning("Event not added, duplicate event name.");
                return;
            }

            scheduledEvents.Add(ticks, scheduledEvent);
        }

        private uint ConvertGameTimeToTicks(GameTime gameTime)
        {
            return timeManager.gameDateTime.GameTimeToTicks(gameTime);
        }

        public void AddEvent(string eventName, GameTime gameTime, Action callback)
        {
            AddEvent(new ScheduledEvent(eventName, gameTime, callback));
        }

        public void CancelEvent(string eventName)
        {
            for (int i = 0; i < scheduledEvents.Count; i++)
            {
                if (scheduledEvents.Values[i].EventName.Equals(eventName))
                {
                    scheduledEvents.RemoveAt(i);
                }
            }
        }
    }
}