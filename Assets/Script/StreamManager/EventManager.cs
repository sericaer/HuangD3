using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventDef
{ 
    public string title = "title";
    public string content = "content";
    public string[] options = { "opt1"};

    public bool funcPrecondition()
    {
        return true;
    }
}

public partial class StreamManager
{
    public class EventManager
    {
        public static event Action<EventDef> evtNewGMEvent;

        public static void Load(Type[] types)
        {
            Type[] EventTypes = types.Where(x => x.BaseType == typeof(EventDef)).ToArray();
            foreach (Type type in EventTypes)
            {
                EventDef ie = Activator.CreateInstance(type) as EventDef;

                _eventDict.Add(type.Name, ie);
            }
            _eventDict.Add("EventDef", new EventDef());
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
                if (eventDef.funcPrecondition())
                {
                    evtNewGMEvent(eventDef);
                    return;
                }
            }
        }

        private static int _index = 0;
        private static Dictionary<string, EventDef> _eventDict = new Dictionary<string, EventDef>();
    }
}
