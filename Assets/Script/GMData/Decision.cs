using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Dynamic;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Decision
    {
        public Decision()
        {
        }

        public Decision(string name)
        {
            this._name = name;
        }

        public dynamic Info()
        {
            dynamic rslt = new ExpandoObject();
            var dict = (IDictionary<string, object>)rslt;
            dict.Add("name", name);
            return rslt;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        [JsonProperty]
        public string _name;
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class Decisions
    {
        public static event Action<Func<dynamic>> evtAdd;
        public static event Action<string> evtDel;


        public void Add(Decision decision)
        {
            _list.Add(decision);
            if (evtAddProv != null)
            {
                evtAdd(decision.Info);
            }

        }

        public void Del(Decision decision)
        {
            _list.Remove(decision);
            evtDel(decision._name);
        }

        public Decision[] All
        {
            get
            {
                return _list.ToArray();
            }
        }


        [JsonProperty]
        private List<Decision> _list = new List<Decision>();
        private object evtAddProv;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (evtAddProv == null)
            {
                return;
            }

            foreach (var prov in _list)
            {
                evtAdd(prov.Info);
            }
        }
    }
}
