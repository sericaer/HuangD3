﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Dynamic;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Provinces
    {
        public static event Action<Func<ProvInfo>> evtAddProv;
        public static event Action<string> evtDelProv;
        

        public void Add(Province prov)
        {
            _list.Add(prov);
            if(evtAddProv != null)
            {
                evtAddProv(prov.GetInfo);
            }

        }

        public void Del(Province prov)
        {
            _list.Remove(prov);
            evtDelProv(prov._name);
        }

        public Province[] All
        {
            get
            {
                return _list.ToArray();
            }
        }


        [JsonProperty]
        private List<Province> _list = new List<Province>();

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if(evtAddProv == null)
            {
                return;
            }

            foreach(var prov in _list)
            {
                evtAddProv(prov.GetInfo);
            }
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Province
    {
        public static Func<string[]> CurrentCountyFlags;

        public Province()
        {
        }

        public Province(IDictionary<string, object> param)
        {
            _name = param["name"] as string;
            _taxbase = Int32.Parse((string)param["taxbase"]);
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public dynamic info
        {
            get
            {
                dynamic rslt = new ExpandoObject();
                var dict = (IDictionary<string, object>)rslt;
                dict.Add("name", name);
                dict.Add("taxbase", _taxbase);
                dict.Add("tax", tax);
                return rslt;
            }
        }
        public ProvInfo GetInfo()
        {
            return new ProvInfo { name = _name, pop = _taxbase};
        }

        public double tax
        {
            get
            {
                double rslt = 0;
                foreach(var elem in taxdetail)
                {
                    rslt += elem.Item2;
                }

                return rslt;
            }
        }

        public Tuple<string, double>[] taxdetail
        {
            get
            {
                var rslt = new List<Tuple<string, double>>();
                rslt.Add(new Tuple<string, double>("BASE", _taxbase));

                string[] flags = CurrentCountyFlags();
                foreach (var flagname in flags)
                {
                    var flag = HuangDAPI.DefCountryFlag.Find(flagname);
                    if (flag.affect.ProvinceTax != null)
                    {
                        rslt.Add(new Tuple<string, double>(flagname, flag.affect.ProvinceTax((double)_taxbase)));
                    }
                }

                return rslt.ToArray();
            }
        }

        [JsonProperty]
        public string _name;

        [JsonProperty]
        int _taxbase;
    }

    public class ProvInfo
    {
        public string name;
        public int pop;
    }

}
