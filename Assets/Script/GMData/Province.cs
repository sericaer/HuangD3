using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

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
            evtAddProv(prov.GetInfo);
        }

        public void Del(Province prov)
        {
            _list.Remove(prov);
            evtDelProv(prov._name);
        }


        [JsonProperty]
        private List<Province> _list = new List<Province>();

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            foreach(var prov in _list)
            {
                evtAddProv(prov.GetInfo);
            }
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Province
    {
        public Province(string name, int pop)
        {
            _name = name;
            _pop = pop;
        }

        public ProvInfo GetInfo()
        {
            return new ProvInfo { name = _name, pop = _pop};
        }

        [JsonProperty]
        public string _name;

        [JsonProperty]
        int _pop;
    }

    public class ProvInfo
    {
        public string name;
        public int pop;
    }

}
