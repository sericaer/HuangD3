using System;
using System.Collections;
using System.Collections.Generic;
using GMDATA;
using UnityEngine;
using UnityEngine.UI;

public class PersonInOfficeLogic : MonoBehaviour
{
    public static GameObject newInstance(Person newPerson, Transform transform)
    {
        var personUI = Instantiate(Resources.Load("Prefabs/Office/PersonInOffice"), transform) as GameObject;
        if (newPerson != null)
        {
            personUI.name = newPerson.name;
            personUI.GetComponent<PersonInOfficeLogic>().personInfo = newPerson.info;
        }
        else
        {
            personUI.name = "--";
        }

        return personUI;
    }

    public static PersonInOfficeLogic Find(string name)
    {
       return list.Find(x => x.name == name);
    }

    public void PersonFactionChange(Person person, string faction)
    {
        factionName.text = faction;
    }

    void Awake()
    {
        transform.Find("reserve1").gameObject.SetActive(false);
        transform.Find("reserve2").gameObject.SetActive(false);

        personName = transform.Find("name").GetComponent<Text>();
        //personScore = transform.Find("score");
        factionName = transform.Find("faction").Find("value").GetComponent<Text>();

        list.Add(this);
    }

    // Use this for initialization
    void Start () 
    {
        personName.text = personInfo["name"] as string;
        factionName.text = personInfo["faction"] as string;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private void OnDestroy()
    {
        list.Remove(this);
    }

    private Text personName;
    private Text factionName;
    private static List<PersonInOfficeLogic> list = new List<PersonInOfficeLogic>();

    private Dictionary<string, object> personInfo;
}
