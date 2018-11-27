using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GMDATA;
using System;
using System.Collections.Generic;

public class MainScene : MonoBehaviour
{
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

        CountryFlag.evtAddFlag += (string flagname) =>{
            CountryStatusLogic.OnAddFlag(flagname);
        };
        CountryFlag.evtDelFlag += (string flagname) => {
            CountryStatusLogic.OnDelFlag(flagname);
        };

        var BtnSave = GameObject.Find("Canvas/PanelCenter/BtnSave").GetComponent<Button>();
        {
            BtnSave.onClick.AddListener(() =>
            {
                GMData.Save();
            });
        }

        Timer.evtOnTimer += GMData.Inist.date.Increase;
        Timer.evtOnTimer += StreamManager.EventManager.OnTimer;
        Timer.evtOnTimer += HuangDAPI.DefDecision.OnTimer;

        Timer.Register("DATE:*/1/2", () =>{
            string desc = "";
            double value = 0;
            foreach(var elem in GMData.Inist.economy.funcIncomeDetail())
            {
                desc += elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                value += elem.Item2;
            }

            desc += "TOTAL: " + value.ToString();

            var opts = new List<Tuple<string, Action>>();
            opts.Add(new Tuple<string, Action>("CONFIRM", () => { GMData.Inist.economy.current += (int)value; }));

            StreamManager.EventManager.AddEvent("TITLE_YEAR_INCOME_REPORT", desc, opts);
        });

        Timer.Register("DATE:*/*/1", () => {
            GMData.Inist.economy.current -= GMData.Inist.military.current;
        });

        Timer.Register("DATE:*/*/*", () =>{
            if (GMData.Inist.emperor.heath <= 0)
            {
                var opts = new List<Tuple<string, Action>>();
                opts.Add(new Tuple<string, Action>("CONFIRM", () => {
                    Timer.Clear();

                    SceneManager.LoadSceneAsync("EndScene");
                }));

                StreamManager.EventManager.AddEvent("TITLE_EMPEROR_DIE", "CONTENT_TITLE_EMPEROR_DIE", opts);

            }
        });

        StreamManager.EventManager.evtNewGMEvent += this.OnNewGMEvent;
        DialogLogic.evntCreate += Timer.Pause;
        DialogLogic.evntDestory += Timer.unPause;
        HuangDAPI.DefCountryFlag.evtEnable += GMData.Inist.countryFlag.Add;
        HuangDAPI.DefCountryFlag.evtDisable += GMData.Inist.countryFlag.Del;

        HuangDAPI.DefDecision.evtEnablePublish += GMData.Inist.decisions.EnablePublish;
        HuangDAPI.DefDecision.evtEnableCancel  += GMData.Inist.decisions.EnableCancel;

        HuangDAPI.Stability.evtChange += (int value) => {

            var opts = new List<Tuple<string, Action>>();
            opts.Add(new Tuple<string, Action>("CONFIRM", () => { GMData.Inist.stability.current += value; }));

            if (value > 0)
            {
                StreamManager.EventManager.AddEvent("TITLE_STABILITY_INCREASE", "CONTENT_STABILITY_INCREASE" + value.ToString(), opts);
            }
            if (value < 0)
            {
                StreamManager.EventManager.AddEvent("TITLE_STABILITY_DECREASE", "CONTENT_STABILITY_DECREASE" + value.ToString(), opts);
            }
        };

        HuangDAPI.Stability.stability = GMData.Inist.stability;

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

    void OnNewGMEvent(string titile, string content, List<Tuple<string, Action>> listOptions)
    {
        DialogLogic.newDialogInstace(titile, content, listOptions);
    }

    private GameObject _uiPanelCenter;
    //private GameObject _uiDialog;


}
