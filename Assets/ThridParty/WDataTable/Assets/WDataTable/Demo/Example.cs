using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WDT;

public class Example : MonoBehaviour
{
    public WDataTable dataTable;
    public bool testDynamic;

    // Use this for initialization
    void Start()
    {
        IList<IList<object>> datas = new List<IList<object>>();
        IList<WColumnDef> columnDefs = new List<WColumnDef>();
        columnDefs.Add(new WColumnDef() { name = "ID", width = "40"});
        columnDefs.Add(new WColumnDef() { name = "A", elemType = WElemType.BUTTON });
        columnDefs.Add(new WColumnDef() { name = "B" });
        columnDefs.Add(new WColumnDef() { name = "C" });
        columnDefs.Add(new WColumnDef() { name = "D", isSort = false});

        for (int i = 0; i < 120; i++)
        {
            var tdatas = new List<object>
            {
                i + 1,
                "dsada" + i,
                20.1 + i,
                Random.Range(0.0f, 1.0f),
                new Vector3(1, i, 2)
            };
            datas.Add(tdatas);
        }

        GameObject obj = Instantiate(Resources.Load("Prefabs/DataTable"), GameObject.Find("Canvas").transform) as GameObject;
        dataTable = obj.GetComponent<WDataTable>();
        dataTable.rowPrefab = "RowCotainter";
        dataTable.itemHeight = 20;
        dataTable.tableWidth = 600;
        dataTable.tableHeight = 300;
        dataTable.isUseSort = true;
        dataTable.isUseSelect = true;
        dataTable.MsgHandle += HandleTableEvent;
        dataTable.InitDataTable(datas, columnDefs);

        //foreach(var row in dataTable.listRow)
        //{
        //    IDictionary<string, object> rowData = row.Getdata();
        //    (from x in rowData
        //     where x.Key)
        //}
    }

    public void HandleTableEvent(WEventType messageType, params object[] args)
    {
        if (messageType == WEventType.INIT_ELEMENT)
        {
            int rowIndex = (int) args[0];
            int columnIndex = (int) args[1];
            WElement element = args[2] as WElement;
            if (element == null)
                return;
            Text text = element.GetComponent<Text>();
            if (text == null)
                return;
            text.color = columnIndex % 2 == 0 ? Color.blue : Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!testDynamic)
            return;
        dataTable.tableWidth = (int) (Mathf.Sin(Time.time * 2) * 100) + 600;
        dataTable.tableHeight = (int) (Mathf.Sin(Time.time * 2) * 50) + 200;
        dataTable.itemHeight = (int) (Mathf.Cos(Time.time * 2) * 10) + 40;
        dataTable.UpdateSize();
    }
}