using UnityEngine;
using Tools;
using Newtonsoft.Json;
using System;

namespace GMDATA
{
    public class Stability
    {
        public event Action<int> evtChange;

        public int current
        {
            get
            {
                return _current;
            }
            set
            {
                int changed = value - _current;
                evtChange(changed);

                _current = value;
                if (_current > Max)
                {
                    _current = Max;
                }
                if (_current < Min)
                {
                    _current = Min;
                }


            }
        }

        [JsonProperty]
        private int _current = Probability.GetRandomNum(0, 3);

        private const int Max = 5;
        private const int Min = 0;
    }
}