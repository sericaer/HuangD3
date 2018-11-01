﻿using UnityEngine;
using Tools;
using Newtonsoft.Json;

public class Stability
{
    public int current
    {
        get
        {
            return _current;
        }
        set
        {

            _current = value;
            if (_current > Max)
            {
                _current = Max;
            }
            if (_current < Min)
            {
                _current = Min;
            }
        }
    }

    [JsonProperty]
    private int _current = Probability.GetRandomNum(0, 3);

    private const int Max = 5;
    private const int Min = 0;
}
