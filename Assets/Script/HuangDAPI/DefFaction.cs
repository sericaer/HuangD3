using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class Faction
    {
        public static event Action<IDictionary<string, int>> evtPowerChange;

        public static void OnPowerChange()
        {
            evtPowerChange(powerDict);
        }

        public Faction(string name)
        {
            this.name = name;

            list.Add(this);
        }

        public static Faction Find(string name)
        {
            return list.Find(x => x.name == name);
        }

        public static Faction[] All
        {
            get
            {
                return list.ToArray();
            }
        }

        public static IDictionary<string, int> powerDict
        {
            get
            {
                Dictionary<string, int> rslt = new Dictionary<string, int>();
                foreach (var elem in All)
                {
                    rslt.Add(elem.name, elem.power);
                }

                return rslt;
            }
        }

        public double powerPercent
        {
            get
            {
                int num = 0;
                foreach(var elem in powerDetail)
                {
                    num += elem.Item2;
                }

                int den = 0;
                foreach(var elem in HuangDAPI.Office.All)
                {
                    den += elem.power;
                }

                return (double)num/den;
            }
        }

        public int power
        {
            get
            {
                return (from x in powerDetail
                        select x.Item2).Sum();
            }
        }

        public Tuple<string, int>[] powerDetail
        {
            get
            {
                var query = (from x in GMDATA.GMData.Inist.Relationship.person2faction
                             join y in GMDATA.GMData.Inist.Relationship.office2person on x.Key equals y.Value
                             where x.Value == name
                             select y.Key);
                
                var rslt = new List<Tuple<string, int>>();
                foreach(var elem in query)
                {
                    var office = HuangDAPI.Office.Find(elem);
                    rslt.Add(new Tuple<string, int>(office.name, office.power));
                }
                return rslt.ToArray();
            }
        }

        public string name;

        static List<Faction> list = new List<Faction>();
    }
}
