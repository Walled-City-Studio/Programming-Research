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

        private SortedList<int, ScheduledEvent> _scheduledEvents;
        private int _id;

        private void Start()
        {
            _scheduledEvents = new SortedList<int, ScheduledEvent>(new TickComparer());

            var event1 = new ScheduledEvent(
                (gm) => { print("callback of event " + 1); },
                new GameDateTime(5, 5, 5),
                "eventName1"
            );
            var success1 = TryAddEvent(event1, out var id1);
            Debug.Log($"{success1} with id {id1}");

            var event2 = new ScheduledEvent(
                (gm) => { print("callback of event " + 2); },
                new GameDateTime(12, 0, 0),
                "eventName2"
            );
            var success2 = TryAddEvent(event2, out var id2);
            Debug.Log($"{success2} with id {id2}");

            var event3 = new ScheduledEvent(
                (gm) => { print("callback of event " + 3); },
                new GameDateTime(24, 60, 30),
                "eventName3"
            );
            var success3 = TryAddEvent(event3, out var id3);
            Debug.Log($"{success3} with id {id3}");

            var event4 = new ScheduledEvent(
                (gm) => { print("callback of event " + 4); },
                new GameDateTime(0, 30, 0),
                "eventName4"
            );
            var success4 = TryAddEvent(event4, out var id4);
            Debug.Log($"{success4} with id {id4}");

            print("Event count: " + _scheduledEvents.Count);
            foreach (var scheduledEvent in _scheduledEvents)
            {
                print("Key: " + scheduledEvent.Key);
                print("Time: " + scheduledEvent.Value.GameDateTime.ToString());
            }
        }

        private void OnEnable()
        {
            timeManager.onMinute += CallEvent;
        }

        private void OnDisable()
        {
            timeManager.onMinute -= CallEvent;
        }

        public void CallEvent(GameDateTime gameDateTime)
        {
            if (_scheduledEvents.Any())
            {
                // CallNextEvent(gameDateTime);
            }
        }

        // Call all events scheduled for the current gameDateTime
        private void CallNextEvent(GameDateTime gameDateTime)
        {
            while (true)
            {
                var scheduledEvent = _scheduledEvents.First();
                var @event = scheduledEvent.Value;

                bool isPast = gameDateTime.CompareTo(@event.GameDateTime) > 0;
                if (isPast)
                {
                    @event.Callback(gameDateTime);
                    RemoveEvent(scheduledEvent.Key);

                    Debug.Log($"Executed event: {@event.EventName ?? ""} at {@event.GameDateTime}");

                    continue;
                }

                break;
            }
        }

        public bool TryAddEvent(ScheduledEvent scheduledEvent, out int id)
        {
            id = -1;

            var isPast = timeManager.Now().CompareTo(scheduledEvent.GameDateTime) > 0;
            if (isPast)
            {
                Debug.LogWarning("Can't schedule an event in the past.");
                return false;
            }

            if (_scheduledEvents.Any(@event => scheduledEvent.EventName.Equals(@event.Value.EventName)))
            {
                Debug.LogWarning("Event not added, duplicate event name.");
                return false;
            }

            id = _id++;
            _scheduledEvents.Add(id, scheduledEvent);
            return true;
        }

        public bool RemoveEvent(int id)
        {
            return _scheduledEvents.Remove(id);
        }
    }
}