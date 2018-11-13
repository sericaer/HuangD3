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

        _uiGMTime = GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>();
        _uiStability = GameObject.Find("Canvas/PanelTop/Stability/value").GetComponent<Text>();
        _uiEconomy  = GameObject.Find("Canvas/PanelTop/Economy/value").GetComponent<Text>();

        _uiEmperorName = GameObject.Find("Canvas/PanelEmperor/emperorName/value").GetComponent<Text>();
        _uiEmperorAge = GameObject.Find("Canvas/PanelEmperor/emperorDetail/age/value").GetComponent<Text>();
        _uiEmperorHeath = GameObject.Find("Canvas/PanelEmperor/emperorDetail/heath/slider").GetComponent<Slider>();

        _uiPanelCenter = GameObject.Find("Canvas/PanelCenter");

        _PanelCountry = GameObject.Find("Canvas/PanelCountry").gameObject;
        _PanelEmperor = GameObject.Find("Canvas/PanelEmperor/");
        _PanelEmperorDetail = _PanelEmperor.transform.Find("emperorDetail").gameObject;
        {
            _PanelEmperor.GetComponent<Button>().onClick.AddListener(() =>
            {
                _PanelEmperorDetail.SetActive(!_PanelEmperorDetail.activeSelf);
                _PanelCountry.SetActive(!_PanelCountry.activeSelf);
            });
        }

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

        CountryFlag.evtAddFlag += (string flagname) => {
            _PanelEmperorDetail.SetActive(true);
            _PanelCountry.SetActive(true);
            CountryStatusLogic.OnAddFlag(flagname);
        };

        CountryFlag.evtDelFlag += CountryStatusLogic.OnDelFlag;

        if (InitScene.isNew)
        {
            _gmData = GMData.NewGMData(InitScene.dynastyName, InitScene.yearName, InitScene.emperorName);
        }
        else
        {
            _gmData = GMData.Load();
        }

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

        StreamManager.EventManager.evtNewGMEvent += (string v1, string v2, List<Tuple<string, Action>> v3) => { Timer.Pause(); };
        StreamManager.EventManager.evtNewGMEvent += this.OnNewGMEvent;
        DialogLogic.evntDestory += Timer.unPause;
        HuangDAPI.DefCountryFlag.evtEnable += _gmData.countryFlag.Add;
        HuangDAPI.DefCountryFlag.evtDisable += _gmData.countryFlag.Del;

        _gmData.emperor.CurrentCountyFlags = _gmData.countryFlag.current;
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
        _PanelEmperorDetail.SetActive(false);
        _PanelCountry.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        _uiGMTime.text = _gmData.dynastyName + _gmData.yearName + _gmData.date;
        _uiStability.text = _gmData.stability.current.ToString();
        _uiEconomy.text = _gmData.economy.current.ToString();

        _uiEmperorName.text = _gmData.emperor.name;
        _uiEmperorAge.text  = _gmData.emperor.age.ToString();
        _uiEmperorHeath.value = _gmData.emperor.heath;
        _uiEmperorHeath.maxValue = _gmData.emperor.heathMax;

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

    private Text _uiGMTime;
    private Text _uiStability;
    private Text _uiEconomy;

    private Text _uiEmperorName;
    private Text _uiEmperorAge;
    private Slider _uiEmperorHeath;

    private GameObject _uiPanelCenter;
    private GameObject _uiDialog;
    private GameObject _PanelCountry;
    private GameObject _PanelEmperor;
    private GameObject _PanelEmperorDetail;


}
