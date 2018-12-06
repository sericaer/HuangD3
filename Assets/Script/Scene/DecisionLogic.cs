using System;
using System.Collections;
using System.Collections.Generic;
using GMDATA;
using UnityEngine;
using UnityEngine.UI;

public class DecisionLogic : MonoBehaviour
{
    public static Action<string> evtPublish;
    public static Action<string> evtCancel;

    public void OnStateChange(Decision.State currState)
    {
        switch (currState)
        {
            case Decision.State.PUBLISH_ED:
                {
                    this.btnPublish.gameObject.SetActive(false);
                    this.btnCancel.gameObject.SetActive(true);
                    this.btnCancel.interactable = false;
                }
                break;
            case Decision.State.CANCEL_ENABLE:
                {
                    this.btnPublish.gameObject.SetActive(false);
                    this.btnCancel.gameObject.SetActive(true);
                    this.btnCancel.interactable = true;
                }
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        Debug.Log("DecisionLogic Awake");

        title = this.transform.Find("Text").GetComponent<Text>();

        slider = this.transform.Find("Slider").GetComponent<Slider>();

        btnPublish = this.transform.Find("BtnPublish").GetComponent<Button>();
        btnPublish.onClick.AddListener(onBtnPublishClick);

        btnCancel = this.transform.Find("BtnCancel").GetComponent<Button>();
        btnCancel.onClick.AddListener(OnBtnCancelClick);

        btnCancel.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);

    }

	// Use this for initialization
	void Start ()
    {
         
        Debug.Log("DecisionLogic Start");
    }
	
	// Update is called once per frame
	void Update ()
    {
        title.text = _funcTitle();
        //MyGame.DecisionProcess decision = MyGame.DecisionProcess.current.Find(x => x.name == decisionname);
        //title.text = HuangDAPI.DECISION.All[name]._funcTitle();

        //switch (decision.state)
        //{
        //    case MyGame.DecisionProcess.ENUState.Publishing:
        //        {
        //            btnCancel.gameObject.SetActive(false);
        //            btnPublish.gameObject.SetActive(false);

        //            slider.gameObject.SetActive(true);
        //            slider.maxValue = decision.maxTimes;
        //            slider.value = decision.lastTimes;
        //        }
        //        break;

        //    case MyGame.DecisionProcess.ENUState.UnPublish:
        //        {
        //            btnCancel.gameObject.SetActive(false);
        //            slider.gameObject.SetActive(false);

        //            btnPublish.gameObject.SetActive(true);
        //            btnPublish.interactable = decision.CanPublish();
        //        }
        //        break;

        //    case MyGame.DecisionProcess.ENUState.Published:
        //        {
        //            btnPublish.gameObject.SetActive(false);
        //            slider.gameObject.SetActive(false);

        //            btnCancel.gameObject.SetActive(true);
        //            btnCancel.interactable = decision.CanCancel();
        //        }
        //        break;
        //}
        //btnDo.interactable = decplan.IsEnable() && !GameFrame.eventManager.isEventDialogExit;
	}

    internal static GameObject newInstance(Decision decision, Transform parent)
    {
        var decisionUI = Instantiate(Resources.Load("Prefabs/decision"), parent) as GameObject;
        decisionUI.name = decision.name;
        decisionUI.GetComponent<DecisionLogic>()._funcTitle = () =>{
            return decision.title;
        };

        return decisionUI;
    }

    public void onBtnPublishClick()
    {
        Debug.Log("publish Decision:" + this.name);
        evtPublish(this.name);
    }

    public void OnBtnCancelClick()
    {
        Debug.Log("cancel Decision:" + this.name);
        evtCancel(this.name);
        //MyGame.DecisionProcess.current.Find(x => x.name == this.name).Cancel();
    }

    private Button btnPublish;
    private Button btnCancel;
    private Slider slider;
    private Text title;
    private Func<string> _funcTitle;

    //private MyGame.DecisionPlan decplan;
}
