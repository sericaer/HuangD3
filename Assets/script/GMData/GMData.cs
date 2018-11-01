﻿using System;

using UnityEngine;
using Newtonsoft.Json;

using Tools;
using System.IO;

[JsonObject(MemberSerialization.OptIn)]
public class GMData
{
    public GMData(string dynastyName, string yearName, string emperorName )
    {
        emperor = new Emperor(emperorName, Probability.GetRandomNum(18, 35), Probability.GetRandomNum(6, 10));
        //date = new Date();
    }

    public void Save()
    {
        Debug.Log("Save:"+savePath);
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(savePath + "/game.save", json);
    }

    public static GMData Load()
    {
        Debug.Log("Load:"+savePath);

        string json = File.ReadAllText(savePath+ "/game.save");

        JsonSerializer serializer = new JsonSerializer();
        StringReader sr = new StringReader(json);
        return serializer.Deserialize(new JsonTextReader(sr), typeof(GMData)) as GMData;
    }

    [JsonProperty]
    public Emperor emperor;

    [JsonProperty]
    public string dynastyName;

    [JsonProperty]
    public string yearName;
    //public Date date;

    public delegate void EventGMTimeChange(string gmTime);

    public event EventGMTimeChange eventGMTimeChange;

    private static string savePath = Application.persistentDataPath + "/save";
}

