using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HuangDAPI;

public partial class StreamManager
{
    public class EventManager
    {
        public static event Action<string, string, List<Tuple<string, Action>>> evtNewGMEvent;

        public static void Load(Type[] types)
        {
            Type[] EventTypes = types.Where(x => x.BaseType == typeof(EventDef)).ToArray();
            foreach (Type type in EventTypes)
            {
                EventDef ie = Activator.CreateInstance(type) as EventDef;

                _eventDict.Add(type.Name, ie);
            }
            Debug.Log("Load event count:" + _eventDict.Count);
        }

        public static void OnTimer()
        {
            for (int i = 0; i != _eventDict.Count; i++, _index++)
            {
                if(_index == _eventDict.Count)
                {
                    _index = 0;
                }

                EventDef eventDef = _eventDict.ElementAt(_index).Value;
                if (eventDef._funcPrecondition())
                {
                    evtNewGMEvent(eventDef._funcTitle(), eventDef._funcContent(), eventDef.listOptions);
                    return;
                }
            }
        }

        public static void AddEvent(string title, string content, List<Tuple<string, Action>> opts)
        {
            evtNewGMEvent(title, content, opts);
        }



        private static int _index = 0;
        private static Dictionary<string, EventDef> _eventDict = new Dictionary<string, EventDef>();
    }
}
