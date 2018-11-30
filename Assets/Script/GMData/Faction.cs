using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GMDATA
{
    [JsonObject(MemberSerialization.Fields)]
    public class Faction
    {
        public Faction(string name)
        {
            this.name = name;
        }

        [JsonProperty]
        public string name;
    }

    [JsonObject(MemberSerialization.Fields)]
    public class Factions
    {
        public Factions()
        {
        }

        public void GenerateData()
        {
            list.Add(new Faction("SHI"));
            list.Add(new Faction("XUN"));
            list.Add(new Faction("WAI"));
        }

        public Faction Find(string name)
        {
            return list.Find(x => x.name == name);
        }

        public Faction[] All
        {
            get
            {
                return list.ToArray();
            }
        }

        [JsonProperty]
        private List<Faction> list = new List<Faction>();
    }
}
