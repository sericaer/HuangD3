using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CountryFlag
    {
        public static event Action<string> evtAddFlag;
        public static event Action<string> evtDelFlag;

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

            if(evtAddFlag != null)
            {
                evtAddFlag(name);
            }

        }

        public void Del(string name)
        {
            _names.Remove(name);

            if(evtDelFlag != null)
            {
                evtDelFlag(name);
            }
        }

        public string[] current()
        {
            return _names.ToArray();
        }

        [JsonProperty]
        private List<string> _names = new List<string>();

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            foreach(var name in _names)
            {
                evtAddFlag(name);
            }
        }
    }

}
