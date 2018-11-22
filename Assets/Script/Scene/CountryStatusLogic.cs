using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GMDATA;

public class CountryStatusLogic : MonoBehaviour
{
    public static CountryStatusLogic inst = null;

    public static void OnAddFlag(string name)
    {
        if (inst == null)
            return;
        
        inst.AddFlag(name);
    }

    public static void OnDelFlag(string name)
    {
        if (inst == null)
            return;
        
        inst.DelFlag(name);
    }

    void Awake()
    {
        Debug.Log("CountryStatusLogic Awake");
        this.gameObject.SetActive(false);
        inst = this;

        Debug.Log("flagcout:" + GMData.Inist.countryFlag.names.Count);
        foreach (var name in GMData.Inist.countryFlag.names)
        {
            AddFlag(name);
        }
    }

	// Use this for initialization
	void Start ()
    {
        Debug.Log("CountryStatusLogic Start");

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void AddFlag(string name)
    {        
        GameObject Text = new GameObject(name, typeof(RectTransform));
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
        var currFlag = this.transform.Find("Status/" + name);
        if (currFlag != null)
        {
            Destroy(currFlag.gameObject);
        }
    }


}
