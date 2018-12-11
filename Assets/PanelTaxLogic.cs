using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelTaxLogic : AwakeTaskBehaviour<PanelTaxLogic>
{
    public void TaxChanged(dynamic param)
    {
        Slider slider = null;
        if (param.name == "SHIZTax")
        {
            slider = _SliderSHIZ;
        }
        else if (param.name == "MIINTax")
        {
            slider = _SliderMIIN;
        }

        slider.maxValue = param.max;
    }

    public static event Action<Tuple<string, float>> evtPlayerChangeTax;

    private void Awake()
    {
        Inst = this;

        this.gameObject.SetActive(false);

        _SliderSHIZ = this.transform.Find("SHIZTax").GetComponent<Slider>();
        _SliderSHIZ.onValueChanged.AddListener((float value) =>{
            evtPlayerChangeTax(new Tuple<string, float>("SHIZ", value));
        });

        _SliderMIIN = this.transform.Find("MIINTax").GetComponent<Slider>();
        _SliderMIIN.onValueChanged.AddListener((float value) => {
            evtPlayerChangeTax(new Tuple<string, float>("MIIN", value));
        });

    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private Slider _SliderSHIZ;
    private Slider _SliderMIIN;
}
