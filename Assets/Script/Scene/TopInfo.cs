using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using GMDATA;

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
                        if (GMData.Inist == null)
                            return "";
                         return GMData.Inist.stability.current.ToString(); 
                    };
                }
                break;
            case "Economy":
                {
                    funcValue = () => { 
                        if (GMData.Inist == null)
                            return "";
                         return GMData.Inist.economy.current.ToString(); 
                    };

                    funcDetail = () => {

                        if (GMData.Inist == null)
                            return "";
                        string rslt = "INCOME:\n";
                        double income = 0;
                        foreach (var elem in GMData.Inist.economy.funcIncomeDetail())
                        {
                            rslt += "\t" + elem.Item1 + ": " + elem.Item2.ToString() + "\n";
                            income += elem.Item2;
                        }
                        rslt += "INCOME_TOTAL: " + income.ToString() + "\n";

                        rslt += "PAYOUT:\n";
                        double payout = 0;
                        foreach (var elem in GMData.Inist.economy.funcPayoutDetail())
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
                        if (GMData.Inist == null)
                            return "";
                        return GMData.Inist.military.current.ToString(); 
                    };
                }
                break;
            case "Time":
                {
                    funcValue = () => { 
                        if (GMData.Inist == null)
                            return "";
                        return GMData.Inist.dynastyName + GMData.Inist.yearName + GMData.Inist.date; 
                    };
                }
                break;
            case "heath":
                {
                    funcValue = () => { 
                        if (GMData.Inist == null)
                            return "";
                        return GMData.Inist.emperor.heath.ToString(); 
                    };

                    funcDetail = () =>{
                        if (GMData.Inist == null)
                            return "";
                        string rslt = "";
                        foreach (var elem in GMData.Inist.emperor.heathdetail)
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
