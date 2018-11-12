﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GMDATA;

public class TianxScene : MonoBehaviour
{
    //public static event Action<string, Func<ProvInfo>> AddProv;

    void Awake()
    {
        _Panel = GameObject.Find("Canvas/Panel");

        Provinces.evtAddProv += AddProv;
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        foreach(var elem in _listProv)
        {
            elem.Update();
        }
	}

    void AddProv(Func<ProvInfo> funcProvInfo)
    {
        _listProv.Add(new ProvUI(_Panel.transform, funcProvInfo));
    }

    GameObject _Panel;
    List<ProvUI> _listProv = new List<ProvUI>();

    class ProvUI
    {
        public ProvUI(Transform parent, Func<ProvInfo> func)
        {
            _UI = CreateUI(parent);
            _func = func;
        }

        public void Update()
        {
            ProvInfo info = _func();
            _UI.GetComponentInChildren<Text>().text = info.name;
        }

        static GameObject CreateUI(Transform parent)
        {
            GameObject button = new GameObject("button", typeof(RectTransform));
            button.AddComponent<Button>();
            button.AddComponent<CanvasRenderer>();
            button.AddComponent<Image>();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
            button.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);

            GameObject Text = new GameObject("Text", typeof(RectTransform));
            Text.AddComponent<CanvasRenderer>();
            Text.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
            var text = Text.AddComponent<Text>();
            text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
            text.color = Color.black;
            text.alignment = TextAnchor.MiddleCenter;
            Text.transform.SetParent(button.transform);
            button.transform.SetParent(parent);
            return button;
        }

        GameObject _UI;
        Func<ProvInfo> _func;
    }
}
