using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Environment.TimeSystem
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField]
        private TimeManager timeManager;

        private Dictionary<TimeSlot, List<ScheduledEvent>> schedule;

        private int id;
        private int prevTicks;

        private void Start()
        {
            if (timeManager == null)
            {
                Debug.LogError($"{nameof(EventManager)} requires an instance of {nameof(TimeManager)}.");
                return;
            }

            schedule = new Dictionary<TimeSlot, List<ScheduledEvent>>();
            prevTicks = timeManager.Settings.initialDateTime.ToTicks();
        }

        // Call all events scheduled for the current gameDateTime
        private void CallNextEvent(GameDateTime gameDateTime)
        {
            if (schedule.Count <= 0)
            {
                return;
            }

            foreach (var scheduleItem in schedule)
            {
                var timeSlot = scheduleItem.Key;
                if (timeSlot.Ticks >= prevTicks && timeSlot.Ticks <= gameDateTime.ToTicks())
                {
                    foreach (var scheduledEvent in schedule[timeSlot])
                    {
                        scheduledEvent.Callback(gameDateTime);
                    }

                    RemoveTimeSlot(timeSlot);
                    break;
                }
            }

            prevTicks = gameDateTime.ToTicks();
        }

        public TimeSlot AddEvent(ScheduledEvent scheduledEvent)
        {
            if (DoesEventNameExist(scheduledEvent.EventName))
            {
                Debug.LogWarning(
                    $"ScheduledEvent added but there is already an event named '{scheduledEvent.EventName}', will still work without issues.");
            }

            if (scheduledEvent.GameDateTime.CompareTo(timeManager.Now()) < 0)
            {
                Debug.LogWarning(
                    "ScheduledEvent is scheduled in the past, it's automatically rescheduled for the next day.\n" +
                    $"ScheduledEvent: {scheduledEvent.GameDateTime.ToString("HH:mm:ss")} Now: {timeManager.Now().ToString("HH:mm:ss")}."
                );
            }

            // If the key exists, add it to the associated ScheduledEvent List
            if (IsTimeSlotUsed(scheduledEvent.GameDateTime, out var timeSlot))
            {
                schedule[timeSlot].Add(scheduledEvent);
                return timeSlot;
            }

            // Else create a new entry
            var createdTimeSlot = new TimeSlot(id++, scheduledEvent.GameDateTime.ToTicks());
            schedule.Add(
                createdTimeSlot,
                new List<ScheduledEvent> {scheduledEvent}
            );

            return createdTimeSlot;
        }

        private bool DoesEventNameExist(string eventName)
        {
            foreach (var scheduleItem in schedule)
            {
                foreach (var @event in scheduleItem.Value)
                {
                    if (eventName?.Equals(@event.EventName) ?? false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool RemoveEvent(TimeSlot timeSlot, string eventName)
        {
            if (schedule.TryGetValue(timeSlot, out var events))
            {
                ScheduledEvent @event = events.Single(e => e.EventName != null && e.EventName.Equals(eventName));
                return events.Remove(@event);
            }

            return false;
        }

        public bool RemoveTimeSlot(TimeSlot timeSlot)
        {
            return schedule.Remove(timeSlot);
        }

        private bool IsTimeSlotUsed(GameDateTime gameDateTime, out TimeSlot timeSlot)
        {
            timeSlot = default;

            foreach (var @event in schedule)
            {
                if (@event.Key.Ticks == gameDateTime.ToTicks())
                {
                    timeSlot = @event.Key;
                    return true;
                }
            }

            return false;
        }

        private void OnEnable()
        {
            if (timeManager != null)
            {
                timeManager.onMinute += CallNextEvent;
            }
        }

        private void OnDisable()
        {
            if (timeManager != null)
            {
                timeManager.onMinute -= CallNextEvent;
            }
        }

        public void LogEvents(bool logEvents = false)
        {
            Debug.Log($"Total timeslot count: {schedule.Count}");
            foreach (var scheduledEvent in schedule)
            {
                Debug.Log($"Timeslot {scheduledEvent.Key} with {scheduledEvent.Value.Count} event(s)");
                if (logEvents)
                {
                    foreach (var @event in scheduledEvent.Value)
                    {
                        Debug.Log(@event);
                    }
                }
            }
        }
    }
}