using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Relationship
    {
        public static event Action<string, string> evtOffice2PersonChange;

        public Relationship()
        {
            
        }

        public void Init(GMData gmdata)
        {
            var persons = gmdata.persons.All;
            var offices = gmdata.offices.All;

            for (int i = 0; i < Math.Min(persons.Length, offices.Length); i++)
            {
                office2person.Add(offices[i].name, persons[i].name);
                evtOffice2PersonChange(offices[i].name, persons[i].name);
            }

        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (evtOffice2PersonChange != null)
            {
                foreach (var elem in office2person)
                {
                    evtOffice2PersonChange(elem.Key, elem.Value);
                }
            }
        }

        [JsonProperty]
        public Dictionary<string, string> office2person = new Dictionary<string, string>();
    }
}