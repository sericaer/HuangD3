using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GMDATA;

public class CountryStatusLogic : MonoBehaviour
{
    void Awake()
    {
        Inst = this;
        this.gameObject.SetActive(false);

        Debug.Log("CountryStatusLogic AWAKE");
    }

    private void Start()
    {
        var Toggles = GameObject.Find("Canvas/PanelCountry/").GetComponentsInChildren<Toggle>();
        foreach (var toggle in Toggles)
        {
            if (toggle.isOn)
            {
                GameObject.Find("Canvas/PanelCountry/"+ toggle.name.Replace("Toggle", "Panel")).SetActive(true);
            }

            toggle.onValueChanged.AddListener((bool isOn) => {
                GameObject.Find("Canvas/PanelCountry/" + toggle.name.Replace("Toggle", "Panel")).SetActive(isOn);
            });
        }
    }

    public static CountryStatusLogic Inst;


}

