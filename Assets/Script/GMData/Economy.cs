using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Economy
    {
        public static event Action<int> evtChange;

        public int current
        {
            get
            {
                return _current;
            }
            set
            {
                int changed = value - _current;
                evtChange(changed);

                _current = value;
            }
        }

        [JsonProperty]
        private int _current = Probability.GetRandomNum(50, 100);
    }
}