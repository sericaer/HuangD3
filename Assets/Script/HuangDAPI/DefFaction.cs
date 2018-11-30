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

        public int power
        {
            get
            {
                int rslt = 0;
                foreach(var elem in powerDetail)
                {
                    rslt += elem.Item2;
                }

                return rslt;
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
