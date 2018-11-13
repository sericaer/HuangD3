using System;
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
        Debug.Log("TianxAwake");

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
            button.transform.SetParent(parent);
            button.AddComponent<CanvasRenderer>();
            button.AddComponent<Button>();
            button.AddComponent<Image>();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
            button.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            GameObject Text = new GameObject("Text", typeof(RectTransform));
            Text.transform.SetParent(button.transform);
            Text.AddComponent<CanvasRenderer>();
            Text.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
            Text.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            var text = Text.AddComponent<Text>();
            text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
            text.color = Color.black;
            text.alignment = TextAnchor.MiddleCenter;
            
            button.transform.SetParent(parent);
            return button;
        }

        GameObject _UI;
        Func<ProvInfo> _func;
    }
}
