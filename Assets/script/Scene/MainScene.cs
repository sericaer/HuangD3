using System.Collections;
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

        _uiGMTime = GameObject.Find("Canvas/PanelTop/Time/value").GetComponent<Text>();
        _uiEmperorName = GameObject.Find("Canvas/PanelEmperor/emperorName/value").GetComponent<Text>();
        _uiEmperorAge = GameObject.Find("Canvas/PanelEmperor/emperorDetail/age/value").GetComponent<Text>();
        _uiEmperorHeath = GameObject.Find("Canvas/PanelEmperor/emperorDetail/heath/slider").GetComponent<Slider>();

        var PanelEmperor = GameObject.Find("Canvas/PanelEmperor/");
        {
            PanelEmperor.GetComponent<Button>().onClick.AddListener(() =>
            {
                var PanelEmperorDetail = PanelEmperor.transform.Find("emperorDetail").gameObject;
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
    }


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        _uiEmperorName.text = _gmData.emperor.name;
        _uiEmperorAge.text  = _gmData.emperor.age.ToString();
        _uiEmperorHeath.value = _gmData.emperor.heath;
        _uiEmperorHeath.maxValue = _gmData.emperor.heathMax;
	}

    private GMData _gmData;

    private Text _uiGMTime;
    private Text _uiEmperorName;
    private Text _uiEmperorAge;
    private Slider _uiEmperorHeath;
}
