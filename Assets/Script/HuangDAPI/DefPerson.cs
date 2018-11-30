using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HuangDAPI
{
    public class DefPerson
    {
        public DefPerson(string name)
        {
            this.name = name;
        }

        public Faction faction
        {
            get
            {
                string factionname = GMDATA.GMData.Inist.Relationship.person2faction[name];
                return Faction.Find(factionname);
            }
        }

        public string name;
    }

}
