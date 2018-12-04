using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using xcharts;

public class StatistLogic : AwakeTaskBehaviour<StatistLogic> 
{
    void Awake()
    {
        Inst = this;
        Debug.Log("CountryStatusLogic Awake");

        _uiPieChart = GameObject.Find("Canvas/PieChart").GetComponent<PieChart>();
        _uiPieChart.pieInfo.outsideRadius = 200;
    }

    public void OnFactionPowerChange(IDictionary<string, int> dict)
    {
        _uiPieChart.SetData(dict);
    }

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}



    private PieChart _uiPieChart;
}
