using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CountryTax
    {
        public class TaxElem
        {
            public float max;
            public float min;
            public float curr;
        }

        internal void OnChanged(Tuple<string, float, float> argc)
        {
            if(argc.Item1 == "SHIZ")
            {
                SHIZTax.max = argc.Item2;
                SHIZTax.curr = argc.Item3;
            }
            if (argc.Item1 == "MIIN")
            {
                MIINTax.max = argc.Item2;
                MIINTax.curr = argc.Item3;
            }
        }

        [JsonProperty]
        public TaxElem SHIZTax;

        [JsonProperty]
        public TaxElem MIINTax;


    }
}
