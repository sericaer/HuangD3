﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDATA;

public class OfficeCenterGroup1 : AwakeTaskBehaviour<OfficeCenterGroup1> 
{
    public OfficeLogic Find(string name)
    {
        var rslt = list.Find(x => x.name == name);
        return rslt.GetComponent<OfficeLogic>();
    }

    public void newOffice(Office office)
    {
        var officeUI = OfficeLogic.newInstance(string.Format("Prefabs/Office/{0}", office.group.ToString()), office, this.transform) as GameObject;
        list.Add(officeUI);
    }

    public void PersonChange(string office, Person person)
    {
        var officeUI = Find(office);
        officeUI.OnPersonChange(person);
    }

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    List<GameObject> list = new List<GameObject>();
}
