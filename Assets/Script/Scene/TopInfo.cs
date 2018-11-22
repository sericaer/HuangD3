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
                    funcValue = () => {
                        if (MainScene._gmData == null)
                            return "";
                         return MainScene._gmData.stability.current.ToString(); 
                    };
                }
                break;
            case "Economy":
                {
                    funcValue = () => { 
                        if (MainScene._gmData == null)
                            return "";
                         return MainScene._gmData.economy.current.ToString(); 
                    };

                    funcDetail = () => {

                        if (MainScene._gmData == null)
                            return "";
                        string rslt = "INCOME:\n";
                        int income = 0;
                        foreach (var elem in MainScene._gmData.economy.funcIncomeDetail())
                        {
                            rslt += "\t" + elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                            income += elem.Item2;
                        }
                        rslt += "INCOME_TOTAL: " + income.ToString() + "\n";

                        rslt += "PAYOUT:\n";
                        int payout = 0;
                        foreach (var elem in MainScene._gmData.economy.funcPayoutDetail())
                        {
                            rslt += "\t" + elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                            payout += elem.Item2;
                        }
                        rslt += "PAYOUT_TOTAL: " + income.ToString() + "\n";
                        rslt += "BALANCE: " + (income - payout).ToString();
                        return rslt;
                    };
                }
                break;
            case "Military":
                {
                    funcValue = () => { 
                        if (MainScene._gmData == null)
                            return "";
                        return MainScene._gmData.military.current.ToString(); 
                    };
                }
                break;
            case "Time":
                {
                    funcValue = () => { 
                        if (MainScene._gmData == null)
                            return "";
                        return MainScene._gmData.dynastyName + MainScene._gmData.yearName + MainScene._gmData.date; 
                    };
                }
                break;
            case "heath":
                {
                    funcValue = () => { 
                        if (MainScene._gmData == null)
                            return "";
                        return MainScene._gmData.emperor.heath.ToString(); 
                    };

                    funcDetail = () =>{
                        if (MainScene._gmData == null)
                            return "";
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
