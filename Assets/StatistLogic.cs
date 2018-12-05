using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WDT;
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
    }

    // Use this for initialization
    void Start()
    {
        var btns = GameObject.Find("Canvas/PieChart").GetComponentsInChildren<Button>();
        foreach (var elem in btns)
        {
            elem.onClick.AddListener(() =>
            {

                HuangDAPI.Faction faction = HuangDAPI.Faction.Find(elem.name);
                List<IList<object>> datas = new List<IList<object>>();
                foreach (var p in faction.persons)
                {
                    datas.Add(new List<object> { p.name, p.office.name });
                }
                if(datas.Count == 0)
                {
                    return;
                }

                var columnDefs = new List<WColumnDef>();
                columnDefs.Add(new WColumnDef { name = "person" });
                columnDefs.Add(new WColumnDef { name = "office" });

                GameObject obj = Instantiate(Resources.Load("Prefabs/DataTable"), GameObject.Find("Canvas").transform) as GameObject;
                var dataTable = obj.GetComponent<WDataTable>();
                dataTable.rowPrefab = "RowCotainter";
                dataTable.itemHeight = 20;
                dataTable.tableWidth = 600;
                dataTable.tableHeight = Math.Min(300, (datas.Count + 1) * dataTable.itemHeight);
                dataTable.isUseSort = true;
                dataTable.isUseSelect = true;
                dataTable.isDestroyAble = true;

                dataTable.InitDataTable(datas, columnDefs);
            });
        }
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
