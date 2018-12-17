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
    public static Func<Tuple<string, double>[]> funcMIINMaxDetail;

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
        float value = (float)(from x in funcSHIZMaxDetail()
                           select x.Item2).Sum();
        if(_SliderSHIZ.Lock.CompareTo(value) == 0)
        {
            return;
        }

        _SliderSHIZ.OnLockedChange(value);

        float value2 = (float)(from x in funcMIINMaxDetail()
                              select x.Item2).Sum();
        if (_SliderMIIN.Lock.CompareTo(value2) == 0)
        {
            return;
        }

        _SliderMIIN.OnLockedChange(value2);
	}

    public TwinSlider _SliderSHIZ;
    public TwinSlider _SliderMIIN;
}
