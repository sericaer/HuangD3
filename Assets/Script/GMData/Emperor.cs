using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Emperor
{
    public Func<string[]> CurrentCountyFlags;

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
            int rslt = 0;
            foreach(var elem in heathdetail)
            {
                rslt += elem.Item2;
            }

            return rslt;
        }
    }

    public Tuple<string, int>[] heathdetail
    {
        get
        {
            var rslt = new List<Tuple<string, int>>();
            rslt.Add(new Tuple<string, int>("BASE_VALUE", _heath));

            foreach (var flagname in CurrentCountyFlags())
            {
                var flag = HuangDAPI.DefCountryFlag.Find(flagname);
                if (flag.affect.EmperorHeath != null)
                {
                    rslt.Add(new Tuple<string, int>(flagname, flag.affect.EmperorHeath(_heath)));
                }
            }

            return rslt.ToArray();
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
