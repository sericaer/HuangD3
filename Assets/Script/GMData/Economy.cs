using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Economy
    {
        public int current
        {
            get
            {
                return _current;
            }
            set
            {
                int changed = value - _current;
                _current = value;
            }
        }

        [JsonProperty]
        private int _current = Probability.GetRandomNum(50, 100);
    }
}