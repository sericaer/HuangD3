using UnityEngine;
using Tools;
using Newtonsoft.Json;

namespace HuangDAPI
{
    public class Stability
    {
        public static int current
        {
            get
            {
                return stability.current;
            }
            set
            {

                stability.current = value;
            }
        }

        internal static GMDATA.Stability stability;
    }
}