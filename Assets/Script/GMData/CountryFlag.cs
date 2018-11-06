﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class CountryFlag
{
    public event Action<string> evtAddFlag;
    public event Action<string> evtDelFlag;

    public List<string> names
    {
        get
        {
            return _names;
        }
    }

    public void Add(string name)
    {
        _names.Add(name);
        evtAddFlag(name);
    }

    public void Del(string name)
    {
        _names.Remove(name);
        evtDelFlag(name);
    }

    [JsonProperty]
    private List<string> _names = new List<string>();
}
