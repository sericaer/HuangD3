using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMDATA;
using UnityEngine.UI;

public class OfficeLogic : MonoBehaviour 
{
    public static GameObject newInstance(string perfabsName, dynamic office, Transform parent)
    {
        var officeUI = Instantiate(Resources.Load(perfabsName), parent) as GameObject;
        officeUI.name = office.name;
        return officeUI;
    }

    public void OnPersonChange(Person newPerson)
    {
        Destroy(personUI);
        personUI = PersonInOfficeLogic.newInstance(newPerson, this.transform);
    }

    private void Awake()
    {
        officeName = transform.Find("Text").GetComponent<Text>();
        //personName = transform.Find("value").GetComponent<Text>();
        //personScore = transform.Find("score");
        //factionName = transform.Find("faction");

    }

    // Use this for initialization
    void Start () 
    {
        officeName.text = name;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private GameObject personUI;
    private Text officeName;
    private Text personName;

}
