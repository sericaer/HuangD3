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

        public dynamic info
        {
            get
            {
                dynamic rslt = new ExpandoObject();

                var dict = (IDictionary<string, object>)rslt;
                dict.Add("name", name);

                return rslt;
            }
        }
        public string name;

        static List<Faction> list = new List<Faction>();
    }
}
