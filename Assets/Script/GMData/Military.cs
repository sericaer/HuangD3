using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Military
    {
        public int current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        [JsonProperty]
        private int _current = Probability.GetRandomNum(50, 100);
    }
}