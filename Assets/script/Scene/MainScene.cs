using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public static GMData _gmData = null;

    void Awake()
    {
        if (InitScene.isNew)
        {
            _gmData = new GMData(InitScene.dynastyName, InitScene.yearName, InitScene.emperorName);
        }
        else
        {
            _gmData = GMData.Load();
        }

        _eventManager = new EventManager();

        Timer.evtOnTimer += _gmData.date.Increase;
        Timer.evtOnTimer += _eventManager.OnTimer;

        _eventManager.evtNewGMEvent += (GMEvent evt)=>{ Timer.Pause(); }
        _eventManager.evtNewGMEvent += this.OnNewGMEvent;

        _uiGMTime = GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>();
        _uiStability = GameObject.Find("Canvas/PanelTop/Stability/value").GetComponent<Text>();

        _uiEmperorName = GameObject.Find("Canvas/PanelEmperor/emperorName/value").GetComponent<Text>();
        _uiEmperorAge = GameObject.Find("Canvas/PanelEmperor/emperorDetail/age/value").GetComponent<Text>();
        _uiEmperorHeath = GameObject.Find("Canvas/PanelEmperor/emperorDetail/heath/slider").GetComponent<Slider>();
        _uiPanelCenter = GameObject.Find("Canvas/PanelCenter");

        var BtnSave = GameObject.Find("Canvas/PanelCenter/BtnSave").GetComponent<Button>();
        {
            BtnSave.onClick.AddListener(() =>
            {
                _gmData.Save();
            });
        }

        var PanelEmperor = GameObject.Find("Canvas/PanelEmperor/");
        var PanelEmperorDetail = PanelEmperor.transform.Find("emperorDetail").gameObject;
        {
            PanelEmperor.GetComponent<Button>().onClick.AddListener(() =>
            {
                PanelEmperorDetail.SetActive(!PanelEmperorDetail.activeSelf);
            });
        }


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

        _uiPanelCenter.SetActive(false);
        PanelEmperorDetail.SetActive(false);
    }


    // Use this for initialization
    void Start ()
    {

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

    void OnNewGMEvent(GMEvent gmevent)
    {
        _uiDialog = DialogLogic.newDialogInstace(gmevent.title, gmevent.content, gmevent.options);
    }

    private Text _uiGMTime;
    private Text _uiStability;

    private Text _uiEmperorName;
    private Text _uiEmperorAge;
    private Slider _uiEmperorHeath;

    private GameObject _uiPanelCenter;
    private GameObject _uiDialog;

    private EventManager _eventManager;

}
