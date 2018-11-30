using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class Office
    {
        public enum GROUP
        {
            Center1,
            Center2,
            LOCAL,
        }

        public Office(string name, int power, string group)
        {
            this.name = name;
            this.power = power;
            this.group = (GROUP)Enum.Parse(typeof(GROUP), group);

            list.Add(this);
        }

        public static Office[] All
        {
            get
            {
                return list.ToArray();
            }
        }

        public static Office Find(string name)
        {
            return list.Find(x => x.name == name);
        }


        public dynamic info
        {
            get
            {
                dynamic rslt = new ExpandoObject();

                var dict = (IDictionary<string, object>)rslt;
                dict.Add("name", name);
                dict.Add("power", power);
                dict.Add("group", group);

                return rslt;
            }
        }

        public DefPerson person
        {
            get
            {
                string personname = GMDATA.GMData.Inist.Relationship.office2person[name];
                return GMDATA.GMData.Inist.persons.Find(personname).GetDef();
            }
        }


        public string name;
        public int power;
        public GROUP group;

        static List<Office> list = new List<Office>();
    }
}
