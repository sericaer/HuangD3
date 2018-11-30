using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GMDATA
{
    [JsonObject(MemberSerialization.Fields)]
    public class Person
    {
        public Person(string name)
        {
            this.name = name;
        }

        public Dictionary<string, object> info
        {
            get
            {
                var rslt = new Dictionary<string, object>();
                rslt.Add("name", name);
                rslt.Add("faction", GMData.Inist.Relationship.person2faction[name]);
                return rslt;
            }
        }

        public HuangDAPI.DefPerson GetDef()
        {
            return new HuangDAPI.DefPerson(name);
        }

        [JsonProperty]
        public string name;

    }

    [JsonObject(MemberSerialization.Fields)]
    public class Persons
    {
        public Persons()
        {
        }

        public void GenerateData(int count)
        {
            while(list.Count < count)
            {
                string name = StreamManager.PersonName.GetRandomMale();
                if(list.Find(x => x.name == name) != null)
                {
                    continue;
                }
                list.Add(new Person(name));
            }
        }

        public Person Find(string name)
        {
            return list.Find(x => x.name == name);
        }

        public Person[] All
        {
            get
            {
                return list.ToArray();
            }
        }

        [JsonProperty]
        private List<Person> list = new List<Person>();
    }
}
