using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class TopInfo : MonoBehaviour
{
    public Func<string> funcValue;
    public Func<string> funcDetail;

    void Awake()
    {
        _uiValue = this.transform.Find("value").GetComponent<Text>();
        _uiToolTip = GetComponent<TooltipElement>();
    }

    // Use this for initialization
    void Start()
    {
        switch(this.name)
        {
            case "Stability":
                {
                    funcValue = () => { return MainScene._gmData.stability.current.ToString(); };
                }
                break;
            case "Economy":
                {
                    funcValue = () => { return MainScene._gmData.economy.current.ToString(); };

                    funcDetail = () => {
                        string rslt = "";
                        rslt += "INCOME:\n";
                        int income = 0;
                        foreach (var elem in MainScene._gmData.economy.funcIncomeDetail())
                        {
                            rslt += "\t" + elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                            income += elem.Item2;
                        }
                        rslt += "INCOMETOTAL: " + income.ToString();
                        return rslt;
                    };
                }
                break;
            case "Military":
                {
                    funcValue = () => { return MainScene._gmData.military.current.ToString(); };
                }
                break;
            case "Time":
                {
                    funcValue = () => { return MainScene._gmData.dynastyName + MainScene._gmData.yearName + MainScene._gmData.date; };
                }
                break;
            case "heath":
                {
                    funcValue = () => { return MainScene._gmData.emperor.heath.ToString(); };

                    funcDetail = () =>{
                        string rslt = "";
                        foreach (var elem in MainScene._gmData.emperor.heathdetail)
                        {
                            rslt += elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                        }

                        return rslt;
                    };
                }
                break;
            default: 
                throw new ArgumentException();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(funcValue != null)
        {
            _uiValue.text = funcValue();
        }
        
        if (funcDetail != null)
        {
            _uiToolTip.TooltipText = funcDetail();
        }
    }

    Text _uiValue;
    TooltipElement _uiToolTip;
}
