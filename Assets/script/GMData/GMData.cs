﻿using System;

using UnityEngine;
using Newtonsoft.Json;

using Tools;
using System.IO;
using System.Runtime.Serialization;

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
            countryFlag = new CountryFlag();
            provinces = new Provinces();

            foreach (var elem in StreamManager.ProvinceCSV.Defs)
            {
                provinces.Add(new Province(elem.name, Int32.Parse(elem.pop)));
            }

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
        public CountryFlag countryFlag;

        [JsonProperty]
        public Provinces provinces;

        public delegate void EventGMTimeChange(string gmTime);

        public event EventGMTimeChange eventGMTimeChange;

        private static string savePath = Application.persistentDataPath + "/save";
    }

}