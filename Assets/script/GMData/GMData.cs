using System;

using UnityEngine;
using Newtonsoft.Json;

using Tools;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace GMDATA
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GMData
    {
        public static GMData NewGMData(string dynastyName, string yearName, string emperorName)
        {
            return new GMData(dynastyName, yearName, emperorName);
        }

        public void Save()
        {
            Debug.Log("Save:" + savePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(savePath + "/game.save", json);
        }

        public static GMData Load()
        {
            Debug.Log("Load:" + savePath);

            string json = File.ReadAllText(savePath + "/game.save");

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            return serializer.Deserialize(new JsonTextReader(sr), typeof(GMData)) as GMData;
        }

        private GMData()
        {

        }

        private GMData(string dynastyName, string yearName, string emperorName)
        {
            this.dynastyName = dynastyName;
            this.yearName = yearName;

            emperor = new Emperor(emperorName, Probability.GetRandomNum(18, 35), Probability.GetRandomNum(6, 10));

            date = new Date();
            stability = new Stability();
            economy = new Economy();
            military = new Military();

            countryFlag = new CountryFlag();
            provinces = new Provinces();
            offices = new Offices();

            foreach (var elem in StreamManager.CSVManager.Province)
            {
                provinces.Add(new Province((IDictionary<string, object>)elem));
            }
            foreach (var elem in StreamManager.CSVManager.Office)
            {
                offices.Add(new Office((IDictionary<string, object>)elem));
            }

            economy.funcIncomeDetail = () =>{
                var rlst = new List<Tuple<string, int>>();
                foreach (var prov in provinces.All)
                {
                    rlst.Add(new Tuple<string, int>(prov.name, prov.tax));
                }

                return rlst.ToArray();
            };

            economy.funcPayoutDetail = () => {
                var rlst = new List<Tuple<string, int>>();
                return rlst.ToArray();
            };
        }


        [JsonProperty]
        public Emperor emperor;

        [JsonProperty]
        public string dynastyName;

        [JsonProperty]
        public string yearName;

        [JsonProperty]
        public Date date;

        [JsonProperty]
        public Stability stability;

        [JsonProperty]
        public Economy economy;

        [JsonProperty]
        public Military military;

        [JsonProperty]
        public CountryFlag countryFlag;

        [JsonProperty]
        public Provinces provinces;

        [JsonProperty]
        public Offices offices;

        public delegate void EventGMTimeChange(string gmTime);

        public event EventGMTimeChange eventGMTimeChange;

        private static string savePath = Application.persistentDataPath + "/save";
    }

}