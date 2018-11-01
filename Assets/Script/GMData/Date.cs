using System;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Date
{
    public Date()
    {
        _year = 1;
        _month = 1;
        _day = 1;
    }

    public void Increase()
    {
        if (_day == 30)
        {
            if (_month == 12)
            {
                _year++;
                _month = 1;
            }
            else
            {
                _month++;
            }
            _day = 1;
        }
        else
        {
            _day++;
        }
    }

    public int year
    {
        get
        {
            return _year;
        }
    }

    public int month
    {
        get
        {
            return _month;
        }
    }

    public int day
    {
        get
        {
            return _day;
        }
    }

    public override string ToString()
    {
        return _year.ToString() + "年" + _month + "月" + _day + "日";
    }

    [JsonProperty]
    private int _year;

    [JsonProperty]
    private int _month;

    [JsonProperty]
    private int _day;
}