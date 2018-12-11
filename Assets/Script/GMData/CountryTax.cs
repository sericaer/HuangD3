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
            public double max = 2;
            public double min = 0;
            public double curr = 1;
        }

        public CountryTax()
        {
            SHIZTax = new TaxElem();
            MIINTax = new TaxElem();
        }

        internal void OnChanged(Tuple<string, float, float> argc)
        {
            if(argc.Item1 == "SHIZ")
            {
                SHIZTax.max = Math.Round(argc.Item2, 1);
                SHIZTax.curr = Math.Round(argc.Item3, 1);
            }
            if (argc.Item1 == "MIIN")
            {
                MIINTax.max = Math.Round(argc.Item2, 1);
                MIINTax.curr = Math.Round(argc.Item3, 1);
            }
        }

        [JsonProperty]
        public TaxElem SHIZTax;

        [JsonProperty]
        public TaxElem MIINTax;


    }
}
