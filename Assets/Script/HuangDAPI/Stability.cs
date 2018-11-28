using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace HuangDAPI
{
    public class Stability
    {
        public static event Action<int> evtChange;
        public static Func<GMDATA.Stability> stability;

        public static int current
        {
            get
            {
                return stability().current;
            }
            set
            {
                evtChange(value - stability().current);
            }
        }
    }
}