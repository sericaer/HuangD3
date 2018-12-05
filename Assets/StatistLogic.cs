using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using xcharts;

public class StatistLogic : AwakeTaskBehaviour<StatistLogic> 
{
    public bool isTest;
    public static Func<string, string> funcFactionDetail;
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
        foreach (var elem in _uiPieChart.buttons)
        {
        }
    }

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        //if (isTest)
        //{
        //    var dict = new Dictionary<string, int>();
        //    dict.Add("AAA", 1);
        //    dict.Add("BBB", 2);
        //    OnFactionPowerChange(dict);
        //}
	}



    private PieChart _uiPieChart;
}
