﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
        _gmData = new GMData(InitScene.dynastyName, InitScene.yearName, InitScene.emperorName);

        var Toggles = GameObject.Find("Canvas/PanelRight/").GetComponentsInChildren<Toggle>();
        foreach (var toggle in Toggles)
        {
            if(toggle.isOn)
            {
                SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);
            }

            toggle.onValueChanged.AddListener((bool isOn)=>{
                if (!isOn)
                {
                    return;
                }

                for(int i=0; i<SceneManager.sceneCount; i++)
                {
                    string sceneName = SceneManager.GetSceneAt(i).name;
                    if (sceneName != SceneManager.GetActiveScene().name)
                    {
                        SceneManager.UnloadSceneAsync(sceneName);
                    }
                }

                SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);
                });
        }

        Text GMTime = GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>();
        GMTime.text = _gmData.
        _gmData.EventGMTimeChange += (string gmTime) =>
        {
            GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>().text = gmTime;
        };

        _gmData.emperor.EventAgeChange += (int age) =>
        {
            GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>().text = age;
        };

        _gmData.emperor.EventHeathChange += (int heath) =>
        {
            GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>().text = heath;
        };
    }


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    GMData _gmData;
}
