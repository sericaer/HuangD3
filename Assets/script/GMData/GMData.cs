using System;
using Tools;

public class GMData
{
    public GMData(string dynastyName, string yearName, string emperorName )
    {
        emperor = new Emperor(emperorName, Probability.GetRandomNum(18, 35), Probability.GetRandomNum(6, 10));
    }

    public void Test()
    {
        GMTimeChangeEvent(yearName);
    }

    public Emperor emperor;

    public delegate void EventGMTimeChange(string gmTime);

    public event EventGMTimeChange GMTimeChangeEvent;

    string dynastyName;
    string yearName;
    string emperorName;
}

