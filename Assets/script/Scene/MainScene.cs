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
    }


    // Use this for initialization
    void Start ()
    {
        Debug.Log("START");

        _uiGMTime = GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>();
        _uiStability = GameObject.Find("Canvas/PanelTop/Stability/value").GetComponent<Text>();

        _uiEmperorName = GameObject.Find("Canvas/PanelEmperor/emperorName/value").GetComponent<Text>();
        _uiEmperorAge = GameObject.Find("Canvas/PanelEmperor/emperorDetail/age/value").GetComponent<Text>();
        _uiEmperorHeath = GameObject.Find("Canvas/PanelEmperor/emperorDetail/heath/slider").GetComponent<Slider>();
        _uiPanelCenter = GameObject.Find("Canvas/PanelCenter");

        var PanelCountry = GameObject.Find("Canvas/PanelCountry").gameObject;
        var PanelEmperor = GameObject.Find("Canvas/PanelEmperor/");
        var PanelEmperorDetail = PanelEmperor.transform.Find("emperorDetail").gameObject;
        {
            PanelEmperor.GetComponent<Button>().onClick.AddListener(() =>
            {
                PanelEmperorDetail.SetActive(!PanelEmperorDetail.activeSelf);
                PanelCountry.SetActive(!PanelCountry.activeSelf);
            });
        }

        CountryFlag.evtAddFlag += (string flagname) => {
            PanelEmperorDetail.SetActive(true);
            PanelCountry.SetActive(true);
            CountryStatusLogic.OnAddFlag(flagname);
        };

        CountryFlag.evtDelFlag += CountryStatusLogic.OnDelFlag;

        if (InitScene.isNew)
        {
            _gmData = new GMData(InitScene.dynastyName, InitScene.yearName, InitScene.emperorName);
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

        var Toggles = GameObject.Find("Canvas/PanelRight/").GetComponentsInChildren<Toggle>();
        foreach (var toggle in Toggles)
        {
            if (toggle.isOn)
            {
                SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);
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

                SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);
            });
        }

        _uiPanelCenter.SetActive(false);
        PanelEmperorDetail.SetActive(false);
        PanelCountry.SetActive(false);

        Timer.evtOnTimer += _gmData.date.Increase;
        Timer.evtOnTimer += StreamManager.EventManager.OnTimer;

        StreamManager.EventManager.evtNewGMEvent += (string v1, string v2, List<Tuple<string, Action>> v3) => { Timer.Pause(); };
        StreamManager.EventManager.evtNewGMEvent += this.OnNewGMEvent;
        DialogLogic.evntDestory += Timer.unPause;
        HuangDAPI.DefCountryFlag.evtEnable += _gmData.countryFlag.Add;
        HuangDAPI.DefCountryFlag.evtDisable += _gmData.countryFlag.Del;



        _gmData.emperor.CurrentCountyFlags = _gmData.countryFlag.current;
        Stability.evtChange += (int value) => {

            var opts = new List<Tuple<string, Action>>();
            opts.Add(new Tuple<string, Action>("CONFIRM", () => { }));

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        _uiGMTime.text = _gmData.dynastyName + _gmData.yearName + _gmData.date;
        _uiStability.text = _gmData.stability.current.ToString();

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

    private Text _uiEmperorName;
    private Text _uiEmperorAge;
    private Slider _uiEmperorHeath;

    private GameObject _uiPanelCenter;
    private GameObject _uiDialog;

}
