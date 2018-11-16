using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class TopInfo : MonoBehaviour
{
    public Func<string> funcValue;
    public Func<string> funcDetail;

    // Use this for initialization
    void Start()
    {
        _uiValue = this.transform.Find("value").GetComponent<Text>();
        _uiToolTip = GetComponent<TooltipElement>();
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
