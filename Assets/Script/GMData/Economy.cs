using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Tools;
using Newtonsoft.Json;


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

        public Func<Tuple<string, int>[]> funcIncomeDetail;
        public Func<Tuple<string, int>[]> funcPayoutDetail;

        [JsonProperty]
        private int _current = Probability.GetRandomNum(50, 100);
    }
}