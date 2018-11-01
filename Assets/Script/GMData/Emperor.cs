using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Emperor
{
    public string name
    {
        get
        {
            return _name;
        }
    }

    public int age
    {
        get
        {
            return _age;
        }
    }

    public int heath
    {
        get
        {
            return _heath;
        }
    }

    public int heathMax
    {
        get
        {
            return _heath;
        }
    }

    public Emperor(string name, int age, int heath)
    {
        _name = name;
        _age = age;
        _heath = heath;

    }

    [JsonProperty]
    private string _name;

    [JsonProperty]
    private int _age;

    [JsonProperty]
    private int _heath;
}
