using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mopsicus.TwinSlider;

public class PanelTaxLogic : AwakeTaskBehaviour<PanelTaxLogic>
{
    public static event Action<Tuple<string, float, float>> evtPlayerChangeTax;

    public static Func< Tuple<string, double>[] > funcSHIZMaxDetail;
    //public Tuple<string, double> funcMIINMax = 1.0f;

    private void Awake()
    {
        Inst = this;

        this.gameObject.SetActive(false);

        _SliderSHIZ = this.transform.Find("SHIZTax").GetComponent<TwinSlider>();
        _SliderSHIZ.OnSliderChange = ((float value1, float value2) =>{
            evtPlayerChangeTax(new Tuple<string, float, float>("SHIZ", value2, value1));
        });

        _SliderMIIN = this.transform.Find("MIINTax").GetComponent<TwinSlider>();
        _SliderMIIN.OnSliderChange = ((float value1, float value2) => {
            evtPlayerChangeTax(new Tuple<string, float, float>("MIIN", value2, value1));
        });

    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        float value = 1f + (float)(from x in funcSHIZMaxDetail()
                           select x.Item2).Sum();
        if(Math.Abs(currLockedValue - value) < float.MinValue)
        {
            return;
        }

        currLockedValue = value;
        _SliderSHIZ.OnLockedChange(currLockedValue);
	}

    private float currLockedValue;
    private TwinSlider _SliderSHIZ;
    private TwinSlider _SliderMIIN;
}
