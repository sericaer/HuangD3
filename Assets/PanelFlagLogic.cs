using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFlagLogic : AwakeTaskBehaviour<PanelFlagLogic>
{
    public void AddFlag(string name)
    {
        Debug.Log("PanelFlagLogic AddFlag");

        GameObject Text = new GameObject(name, typeof(RectTransform));
        Text.AddComponent<CanvasRenderer>();
        Text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);

        var text = Text.AddComponent<Text>();
        text.text = name;
        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;


        var desc = HuangDAPI.DefCountryFlag.Find(name).describe;
        if (desc != null)
        {
            var tooltip = Text.AddComponent<TooltipElement>();
            tooltip.TooltipText = desc();
        }

        Text.transform.SetParent(this.transform);
    }

    public void DelFlag(string name)
    {
        var currFlag = this.transform;
        if (currFlag != null)
        {
            Destroy(currFlag.gameObject);
        }
    }

    void Awake()
    {
        Inst = this;

        Debug.Log("PanelFlagLogic Awake");
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("PanelFlagLogic Start");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private static event Action aWakeTask;
}


//public class CountryStatusLogic : AwakeTaskBehaviour<CountryStatusLogic>
//{
//    public void AddFlag(string name)
//    {
//        GameObject Text = new GameObject(name, typeof(RectTransform));
//        Text.AddComponent<CanvasRenderer>();
//        Text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);

//        var text = Text.AddComponent<Text>();
//        text.text = name;
//        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
//        text.color = Color.black;
//        text.alignment = TextAnchor.MiddleCenter;


//        var desc = HuangDAPI.DefCountryFlag.Find(name).describe;
//        if (desc != null)
//        {
//            var tooltip = Text.AddComponent<TooltipElement>();
//            tooltip.TooltipText = desc();
//        }

//        Text.transform.SetParent(this.transform.Find("Status").transform);
//    }

//    public void DelFlag(string name)
//    {
//        var currFlag = this.transform.Find("Status/" + name);
//        if (currFlag != null)
//        {
//            Destroy(currFlag.gameObject);
//        }
//    }

//    void Awake()
//    {
//        Inst = this;
//        Debug.Log("CountryStatusLogic Awake");
//        this.gameObject.SetActive(false);
//    }

//	// Use this for initialization
//	void Start ()
//    {
//        Debug.Log("CountryStatusLogic Start");

//    }

//	// Update is called once per frame
//	void Update ()
//    {

//	}

//    private static event Action aWakeTask;
//}