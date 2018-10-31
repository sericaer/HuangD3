using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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

    public Emperor(string name, int age, int heath)
    {
        _name = name;
        _age = age;
        _heath = heath;

    }

    private string _name;

    private int _age;

    private int _heath;
}
