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

                if(rslt.Count != 0)
                {
                    rslt.Add(new Tuple<string, double>("BASE", 1.0));
                }
                else
                {
                    rslt.Add(new Tuple<string, double>("", 2.0));
                }

                return rslt.ToArray();
            }
        }

        public Tuple<string, double>[] MIINTaxMAX
        {
            get
            {
                var rslt = new List<Tuple<string, double>>();
                rslt.Add(new Tuple<string, double>("", 2.0));
                return rslt.ToArray();
            }
        }

        [JsonProperty]
        public double SHIZTax = 1;

        [JsonProperty]
        public double MIINTax = 1;


    }
}
