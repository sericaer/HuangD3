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


        public ProvInfo GetInfo()
        {
            return new ProvInfo { name = _name, pop = _taxbase};
        }

        public int tax
        {
            get
            {
                int rslt = 0;
                foreach(var elem in taxdetail)
                {
                    rslt += elem.Item2;
                }

                return rslt;
            }
        }

        public Tuple<string,int>[] taxdetail
        {
            get
            {
                var rslt = new List<Tuple<string, int>>();
                rslt.Add(new Tuple<string, int>("BASE", _taxbase));
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
