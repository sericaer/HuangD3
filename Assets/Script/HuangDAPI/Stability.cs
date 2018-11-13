using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace HuangDAPI
{
    public class Stability
    {
        public static event Action<int> evtChange;

        public static int current
        {
            get
            {
                return stability.current;
            }
            set
            {
                evtChange(value - stability.current);
            }
        }

        internal static GMDATA.Stability stability;
    }
}