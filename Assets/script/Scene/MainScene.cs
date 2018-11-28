using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GMDATA;
using System;
using System.Collections.Generic;

public class MainScene : MonoBehaviour
{
    public static void OnNewGMEvent(string titile, string content, List<Tuple<string, Action>> listOptions)
    {
        DialogLogic.newDialogInstace(titile, content, listOptions);
    }


    void Awake()
    {
        Debug.Log("AWAKE");

        _uiPanelCenter = GameObject.Find("Canvas/PanelCenter");

        var Toggles = GameObject.Find("Canvas/PanelRight/").GetComponentsInChildren<Toggle>();
        foreach (var toggle in Toggles)
        {
            if (toggle.isOn)
            {
                SceneManager.LoadScene(toggle.name, LoadSceneMode.Additive);
            }

            toggle.onValueChanged.AddListener((bool isOn) => {
                if (!isOn)
                {
                    return;
                }

                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    string sceneName = SceneManager.GetSceneAt(i).name;
                    if (sceneName != SceneManager.GetActiveScene().name)
                    {
                        SceneManager.UnloadSceneAsync(sceneName);
                    }
                }

                SceneManager.LoadScene(toggle.name, LoadSceneMode.Additive);
            });
        }
    }


    // Use this for initialization
    void Start ()
    {
        Debug.Log("START");

        var BtnSave = GameObject.Find("Canvas/PanelCenter/BtnSave").GetComponent<Button>();
        {
            BtnSave.onClick.AddListener(() =>
            {
                GMData.Save();
            });
        }

        _uiPanelCenter.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        OnKeyBoard();
    }

    private void OnKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiPanelCenter.SetActive(!_uiPanelCenter.activeSelf);
        }
        
    }


    private GameObject _uiPanelCenter;
    //private GameObject _uiDialog;


}
