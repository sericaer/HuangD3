using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("Canvas/Panel/NewButton").GetComponent<Button>().onClick.AddListener(() => {
            InitScene.nameDynasty = "111";
            InitScene.nameYear = "111";
            InitScene.nameEmperor = "111";

            SceneManager.LoadSceneAsync("InitScene");
            
        });

        GameObject.Find("Canvas/Panel/QuitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });

        //.
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
