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
