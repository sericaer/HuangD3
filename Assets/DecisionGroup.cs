using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDATA;
using System;

public class DecisionGroup : MonoBehaviour 
{
    public static DecisionGroup Inst;

    public static void Task(Action action)
    {
        if (Inst != null)
        {
            action();
        }

        aWakeTask += action;
    }

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

    private void Awake()
    {
        Inst = this;

        if (aWakeTask != null)
        {
            aWakeTask();
        }
    }

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private static event Action aWakeTask;
    private List<GameObject> list = new List<GameObject>();
}
