using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GMDATA;
using System;
using System.Collections.Generic;

public class MainScene : MonoBehaviour
{
    public static GMData _gmData = null;

    void Awake()
    {
        Debug.Log("AWAKE");

        if (InitScene.isNew)
        {
            _gmData = GMData.NewGMData(InitScene.dynastyName, InitScene.yearName, InitScene.emperorName);
        }
        else
        {
            _gmData = GMData.Load();
        }

        _gmData.emperor.CurrentCountyFlags = _gmData.countryFlag.current;


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
            //CountryStatusLogic.inst.gameObject.SetActive(true)
            CountryStatusLogic.OnAddFlag(flagname);
        };

        var BtnSave = GameObject.Find("Canvas/PanelCenter/BtnSave").GetComponent<Button>();
        {
            BtnSave.onClick.AddListener(() =>
            {
                _gmData.Save();
            });
        }

        Timer.evtOnTimer += _gmData.date.Increase;
        Timer.evtOnTimer += StreamManager.EventManager.OnTimer;
        Timer.Register("DATE:*/1/2", () =>{
            string desc = "";
            int value = 0;
            foreach(var prov in _gmData.provinces.All)
            {
                desc += prov.name;
                foreach(var detail in prov.taxdetail)
                {
                    desc += detail.Item1 + ": " + detail.Item2.ToString() + ", ";
                    value += detail.Item2;
                }
                desc.TrimEnd(' ');
                desc.TrimEnd(',');
                desc += "\n";
            }

            desc += "TOTAL: " + value.ToString();

            var opts = new List<Tuple<string, Action>>();
            opts.Add(new Tuple<string, Action>("CONFIRM", () => { _gmData.economy.current += value; }));

            StreamManager.EventManager.AddEvent("TITLE_YEAR_INCOME_REPORT", desc, opts);
        });

        Timer.Register("DATE:*/*/1", () => {
            _gmData.economy.current -= _gmData.military.current;
        });

        StreamManager.EventManager.evtNewGMEvent += this.OnNewGMEvent;
        DialogLogic.evntCreate += Timer.Pause;
        DialogLogic.evntDestory += Timer.unPause;
        HuangDAPI.DefCountryFlag.evtEnable += _gmData.countryFlag.Add;
        HuangDAPI.DefCountryFlag.evtDisable += _gmData.countryFlag.Del;


        HuangDAPI.Stability.evtChange += (int value) => {

            var opts = new List<Tuple<string, Action>>();
            opts.Add(new Tuple<string, Action>("CONFIRM", () => { _gmData.stability.current += value; }));

            if (value > 0)
            {
                StreamManager.EventManager.AddEvent("TITLE_STABILITY_INCREASE", "CONTENT_STABILITY_INCREASE" + value.ToString(), opts);
            }
            if (value < 0)
            {
                StreamManager.EventManager.AddEvent("TITLE_STABILITY_DECREASE", "CONTENT_STABILITY_DECREASE" + value.ToString(), opts);
            }
        };

        HuangDAPI.Stability.stability = _gmData.stability;

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
        _uiDialog = DialogLogic.newDialogInstace(titile, content, listOptions);
    }

    private GameObject _uiPanelCenter;
    private GameObject _uiDialog;


}
