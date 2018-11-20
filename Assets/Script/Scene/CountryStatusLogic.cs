using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CountryStatusLogic : MonoBehaviour
{
    public static CountryStatusLogic inst = null;

    public static void OnAddFlag(string name)
    {
        inst.AddFlag(name);
    }

    public static void OnDelFlag(string name)
    {
        inst.DelFlag(name);
    }

    void Awake()
    {
        Debug.Log("CountryStatusLogic Awake");
        this.gameObject.SetActive(false);
        inst = this;
    }

	// Use this for initialization
	void Start ()
    {
        foreach(var name in MainScene._gmData.countryFlag.names)
        {
            AddFlag(name);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void AddFlag(string name)
    {
        GameObject Text = new GameObject("Text", typeof(RectTransform));
        Text.AddComponent<CanvasRenderer>();
        Text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);

        var text = Text.AddComponent<Text>();
        text.text = name;
        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;


        var desc = HuangDAPI.DefCountryFlag.Find(name).describe;
        if(desc != null)
        {
            var tooltip = Text.AddComponent<TooltipElement>();
            tooltip.TooltipText = desc();
        }

        Text.transform.SetParent(this.transform.Find("Status").transform);
    }

    void DelFlag(string name)
    {
        var currFlag = this.transform.Find(name);
        if (currFlag != null)
        {
            Destroy(currFlag);
        }
    }

    
}
