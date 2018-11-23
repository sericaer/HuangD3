using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvUI : MonoBehaviour 
{
    public static GameObject NewInstance(IDictionary<string, object> infos)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/ProvUI"), GameObject.Find("Canvas/Panel").transform) as GameObject;
        return obj;
    }

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
