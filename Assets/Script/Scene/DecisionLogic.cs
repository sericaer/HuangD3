using System;
using System.Collections;
using System.Collections.Generic;
using GMDATA;
using UnityEngine;
using UnityEngine.UI;

public class DecisionLogic : MonoBehaviour
{
    public static List<DecisionLogic> list = new List<DecisionLogic>();
    public static Action<string> evtPublish;
    public static Action<string> evtCancel;

    public static void OnStateChange(string name, Decision.State currState)
    {
        var Inst = list.Find(x => x.name == name);
        if(Inst == null)
        {
            return;
        }

        switch (currState)
        {
            case Decision.State.PUBLISH_ED:
                {
                    Inst.btnPublish.gameObject.SetActive(false);
                    Inst.btnCancel.gameObject.SetActive(true);
                    Inst.btnCancel.interactable = false;
                }
                break;
            case Decision.State.CANCEL_ENABLE:
                {
                    Inst.btnPublish.gameObject.SetActive(false);
                    Inst.btnCancel.gameObject.SetActive(true);
                    Inst.btnCancel.interactable = true;
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

        list.Add(this);
    }

	// Use this for initialization
	void Start ()
    {
         
        Debug.Log("DecisionLogic Start");
    }
	
	// Update is called once per frame
	void Update ()
    {
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

    void OnDestroy()
    {
        Debug.Log("DecisionLogic Destroy ");
        list.Remove(this);
    }

    internal static object newInstance(Decision decision, Transform parent)
    {
        var decisionUI = Instantiate(Resources.Load("Prefabs/decision"), parent) as GameObject;
        decisionUI.name = decision.name;
        decisionUI.GetComponent<DecisionLogic>().title.text = decision.name;

        return decisionUI;
    }

    internal static void DestroyInstance(string name)
    {
        var Inst = list.Find(x => x.name == name);
        if (Inst == null)
        {
            return;
        }
        list.Remove(Inst);
        Destroy(Inst.gameObject);
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

    //private MyGame.DecisionPlan decplan;
}
