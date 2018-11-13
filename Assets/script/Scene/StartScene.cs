using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    void Awake()
    {
        StreamManager.Load();

        GameObject.Find("Canvas/Panel/NewButton").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("InitScene");
        });

        GameObject.Find("Canvas/Panel/LoadButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");

        });

        GameObject.Find("Canvas/Panel/QuitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
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
