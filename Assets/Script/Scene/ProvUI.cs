using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using GMDATA;


public class ProvUI : MonoBehaviour 
{
    public static ProvUI Inst;
    public static ProvUI NewInstance(IDictionary<string, object> infos)
    {
        if(Inst != null)
        {
            return Inst;
        }

        GameObject gmobj = Instantiate(Resources.Load("Prefabs/ProvUI"), GameObject.Find("Canvas/Panel").transform) as GameObject;
        Inst = gmobj.GetComponent<ProvUI>();
        Inst.name = infos["name"] as string;
        return Inst;
    }

    public Func<double> funcTaxBase;
    public Func<double> funcTaxCurr;
    public Func<string> funcTaxDetail;

    void Awake()
    {
        

    }

    // Use this for initialization
    void Start () 
    {
        this.transform.Find("name").GetComponent<Text>().text = Inst.name;
        this.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(this.gameObject);
        });


    }

    void OnEnable()
    {
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update () 
    {
        this.transform.Find("detail/taxbase/value").GetComponent<Text>().text = funcTaxBase().ToString();
        this.transform.Find("detail/taxcurr/value").GetComponent<Text>().text = funcTaxCurr().ToString();
        this.transform.Find("detail/taxcurr").GetComponent<TooltipElement>().TooltipText = funcTaxDetail();
    }
}
