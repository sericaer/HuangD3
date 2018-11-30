using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDATA;
using System;

public class DecisionGroup : AwakeTaskBehaviour<DecisionGroup> 
{
    public GameObject NewDecisionUI(Decision decision)
    {
        var rslt =  DecisionLogic.newInstance(decision, this.transform);
        list.Add(rslt.gameObject);

        return rslt;
    }

    public void DeleteElem(string name)
    {
        var elem = list.Find(x => x.name == name);
        Destroy(elem);

        list.Remove(elem);
    }

    public void StateChange(string name, Decision.State currState)
    {
        var elem = list.Find(x => x.name == name);
        if(elem != null)
        {
            elem.GetComponent<DecisionLogic>().OnStateChange(currState);
        }

    }

    void Awake()
    {
        Inst = this;
    }

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private List<GameObject> list = new List<GameObject>();
}
