﻿using System;

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
        public static GMData Inist;

        public static void NewGMData(string dynastyName, string yearName, string emperorName)
        {
            Inist = new GMData(dynastyName, yearName, emperorName);
        }

        public static void Save()
        {
            Debug.Log("Save:" + savePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string json = JsonConvert.SerializeObject(Inist);
            File.WriteAllText(savePath + "/game.save", json);
        }

        public static void Load()
        {
            Debug.Log("Load:" + savePath);

            string json = File.ReadAllText(savePath + "/game.save");

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);

            Inist = serializer.Deserialize(new JsonTextReader(sr), typeof(GMData)) as GMData;
            Inist.Initializer();

            Debug.Log("GMData Load");
        }

        private void Initializer()
        {
            economy.funcIncomeDetail = () => {
                var rlst = new List<Tuple<string, double>>();
                foreach (var prov in provinces.All)
                {
                    rlst.Add(new Tuple<string, double>(prov.name, prov.tax));
                }

                return rlst.ToArray();
            };

            economy.funcPayoutDetail = () => {
                var rlst = new List<Tuple<string, double>>();
                rlst.Add(new Tuple<string, double>("Military", military.current));
                return rlst.ToArray();
            };

            emperor.CurrentCountyFlags = countryFlag.current;
            Province.CurrentCountyFlags = countryFlag.current;
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

            Initializer();

            Debug.Log("GMData New");
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

        private static string savePath = Application.persistentDataPath + "/save";
    }

}