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
        _uiValue = GetComponentInChildren<Text>();
        _uiToolTip = GetComponent<TooltipElement>();
    }

    // Update is called once per frame
    void Update()
    {
        _uiValue.text = funcValue();

        if (funcDetail != null)
        {
            _uiToolTip.TooltipText = funcDetail();
        }
    }

    Text _uiValue;
    TooltipElement _uiToolTip;
}
