using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        internal void OnChanged(Tuple<string, float, float> argc)
        {
            if(argc.Item1 == "SHIZ")
            {
                SHIZTax = Math.Round(argc.Item3, 1);
            }
            if (argc.Item1 == "MIIN")
            {
                MIINTax = Math.Round(argc.Item3, 1);
            }
        }

        public Tuple<string, double>[] SHIZTaxMAX
        {
            get
            {
                var rslt = new List<Tuple<string, double>>();
                foreach (var elem in HuangDAPI.Affect.Started)
                {
                    if (elem.Value.SHIZTaxPercent != null)
                    {
                        rslt.Add(new Tuple<string, double>(elem.Key, elem.Value.SHIZTaxPercent(1)));
                    }
                }

                return rslt.ToArray();
            }
        }


        //public double MIINTaxMAX
        //{
        //    get
        //    {

        //    }
        //}

        [JsonProperty]
        public double SHIZTax = 1;

        [JsonProperty]
        public double MIINTax = 1;


    }
}
