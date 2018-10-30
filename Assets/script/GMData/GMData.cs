using System;

public class GMData
{
    public GMData(string dynastyName, string yearName, string emperorName )
    {
        this.dynastyName = dynastyName;
        this.yearName = yearName;
        this.emperorName = emperorName;
    }

    public void Test()
    {
        GMTimeChangeEvent(yearName);
    }

    public delegate void EventGMTimeChange(string gmTime);

    public event EventGMTimeChange GMTimeChangeEvent;

    string dynastyName;
    string yearName;
    string emperorName;
}

