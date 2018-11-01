using System;
using System.Collections;
using System.Collections.Generic;

public class GMEvent
{ 
    public string title = "title";
    public string content = "content";
    public string[] options = { "opt1"};
}

public class EventManager
{
    public event Action<GMEvent> evtNewGMEvent;

    public void OnTimer()
    {
        GMEvent gmevent = new GMEvent();
        evtNewGMEvent(gmevent);
    }
}
