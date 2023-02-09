using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha
{
    public delegate void Callback(System.Object arg);

    public static class EventHandler
    {

        private static Dictionary<EventID, Dictionary<int, List<Delegate>>> eventTable = new Dictionary<EventID, Dictionary<int, List<Delegate>>>();
        public static Dictionary<EventID, Dictionary<int, List<Delegate>>> EventTable => eventTable;
        public static void Subscribe(EventID eventType, Callback handler, int priority = 5)
        {
            lock (eventTable)
            {

                if (!eventTable.ContainsKey(eventType))
                {
                    eventTable.Add(eventType, new Dictionary<int, List<Delegate>>());
                }

                Dictionary<int, List<Delegate>> value = eventTable[eventType];

                if (!value.ContainsKey(priority))
                {
                    value.Add(priority, new List<Delegate>());
                }

                value[priority].Add(handler);

            }
        }

        public static void UnSubscribe(EventID eventType, Callback handler)
        {
            lock (eventTable)
            {
                if (eventTable.ContainsKey(eventType))
                {
                    Dictionary<int, List<Delegate>> value = eventTable[eventType];

                    foreach (KeyValuePair<int, List<Delegate>> entry in value)
                    {
                        entry.Value.Remove(handler);
                    }
                }
            }
        }

        public static void BroadCast(EventID eventType, System.Object arg = null)
        {
            if (eventTable.ContainsKey(eventType))
            {
                Dictionary<int, List<Delegate>> value = eventTable[eventType];

                foreach (KeyValuePair<int, List<Delegate>> entry in value.OrderByDescending(x => x.Key))
                {
                    foreach (Delegate observer in entry.Value.ToList())
                    {
                        observer.DynamicInvoke(arg);
                    }
                }
            }
        }

        public static void CleanUpTable()
        {
            eventTable.Clear();
        }
    }
}