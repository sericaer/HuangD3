﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GMDATA;
using WDT;

public class TianxScene : MonoBehaviour
{
    //public static event Action<string, Func<ProvInfo>> AddProv;
    void Awake()
    {
        Debug.Log("TianxAwake");
    }

    // Use this for initialization
    void Start()
    {
        IList<WColumnDef> columnDefs = null;
        IList<IList<object>> datas = new List<IList<object>>();
        foreach (var prov in MainScene._gmData.provinces.All)
        {
            IDictionary<string, object> dict = (IDictionary<string, object>)prov.info;
            if (columnDefs == null)
            {
                columnDefs = (from x in dict.Keys
                              select new WColumnDef() { name = x }).ToList();
            }

            datas.Add(dict.Values.ToList());
        }

        //IList<IList<object>> datas = new List<IList<object>>();
        //IList<WColumnDef> columnDefs = new List<WColumnDef>();
        //columnDefs.Add(new WColumnDef() { name = "ID", width = "40" });
        //columnDefs.Add(new WColumnDef() { name = "A", elemType = WElemType.BUTTON });
        //columnDefs.Add(new WColumnDef() { name = "B" });
        //columnDefs.Add(new WColumnDef() { name = "C" });
        //columnDefs.Add(new WColumnDef() { name = "D", isSort = false });

        //for (int i = 0; i < 120; i++)
        //{
        //    var tdatas = new List<object>
        //    {
        //        i + 1,
        //        "dsada" + i,
        //        20.1 + i,
        //        UnityEngine.Random.Range(0.0f, 1.0f),
        //        new Vector3(1, i, 2)
        //    };
        //    datas.Add(tdatas);
        //}

        GameObject obj = Instantiate(Resources.Load("Prefabs/DataTable"), GameObject.Find("Canvas/Panel").transform) as GameObject;
        dataTable = obj.GetComponent<WDataTable>();
        dataTable.rowPrefab = "RowCotainter";
        dataTable.itemHeight = 20;
        dataTable.tableWidth = 600;
        dataTable.tableHeight = Math.Min(300, datas.Count*dataTable.itemHeight);
        dataTable.isUseSort = true;
        dataTable.isUseSelect = true;
        dataTable.InitDataTable(datas, columnDefs);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //   void Awake()
    //   {
    //       Debug.Log("TianxAwake");

    //       _Panel = GameObject.Find("Canvas/Panel");

    //       Provinces.evtAddProv += AddProv;
    //   }

    //   // Use this for initialization
    //   void Start ()
    //   {
    //       foreach(var prov in MainScene._gmData.provinces.All)
    //       {
    //           AddProv(prov.GetInfo);
    //       }
    //   }

    //// Update is called once per frame
    //void Update ()
    //   {
    //       foreach(var elem in _listProv)
    //       {
    //           elem.Update();
    //       }
    //}

    //void AddProv(Func<ProvInfo> funcProvInfo)
    //{
    //    _listProv.Add(new ProvUI(_Panel.transform, funcProvInfo));
    //}

    //GameObject _Panel;
    //List<ProvUI> _listProv = new List<ProvUI>();

    //class ProvUI
    //{
    //    public ProvUI(Transform parent, Func<ProvInfo> func)
    //    {
    //        _UI = CreateUI(parent);
    //        _func = func;
    //    }

    //    public void Update()
    //    {
    //        ProvInfo info = _func();
    //        _UI.GetComponentInChildren<Text>().text = info.name;
    //    }

    //    static GameObject CreateUI(Transform parent)
    //    {
    //        GameObject button = new GameObject("button", typeof(RectTransform));
    //        button.transform.SetParent(parent);
    //        button.AddComponent<CanvasRenderer>();
    //        button.AddComponent<Button>();
    //        button.AddComponent<Image>();
    //        button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
    //        button.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

    //        GameObject Text = new GameObject("Text", typeof(RectTransform));
    //        Text.transform.SetParent(button.transform);
    //        Text.AddComponent<CanvasRenderer>();
    //        Text.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
    //        Text.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

    //        var text = Text.AddComponent<Text>();
    //        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
    //        text.color = Color.black;
    //        text.alignment = TextAnchor.MiddleCenter;

    //        button.transform.SetParent(parent);
    //        return button;
    //    }

    //    GameObject _UI;
    //    Func<ProvInfo> _func;
    //}

    private WDataTable dataTable;
}
