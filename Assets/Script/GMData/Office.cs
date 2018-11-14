using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Offices
    {
        //public static event Action<Func<ProvInfo>> evtAddProv;
        //public static event Action<string> evtDelProv;

        public void Add(Office prov)
        {
            _list.Add(prov);
        }

        public void Del(Office prov)
        {
            _list.Remove(prov);
        }

        public Office[] All
        {
            get
            {
                return _list.ToArray();
            }
        }


        [JsonProperty]
        private List<Office> _list = new List<Office>();
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Office
    {
        public Office()
        {
        }

        public Office(IDictionary<string, object> param)
        {
            _name = param["name"] as string;
            _power = Int32.Parse((string)param["power"]);
            _group = (GROUP)Enum.Parse(typeof(GROUP), (string)param["group"]);
            _implevel = (IMPLEVEL)Enum.Parse(typeof(IMPLEVEL), (string)param["importance"]);
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public int power
        {
            get
            {
                return _power;
            }
        }

        public GROUP group
        {
            get
            {
                return _group;
            }
        }

        public IMPLEVEL implevel
        {
            get
            {
                return _implevel;
            }
        }

        public enum GROUP
        {
            Center,
            LOCAL,
        }

        public enum IMPLEVEL
        {
            IMP0,
            IMP1,
        }

        [JsonProperty]
        public string _name;

        [JsonProperty]
        int _power;

        [JsonProperty]
        GROUP _group;

        [JsonProperty]
        IMPLEVEL _implevel;
    }

}
