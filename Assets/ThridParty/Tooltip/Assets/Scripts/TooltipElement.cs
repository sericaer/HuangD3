using System;
using UnityEngine;
using UnityEngine.EventSystems;

/*
    Any element that needs to display a tooltip should attach this script. 
*/

public class TooltipElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string TooltipText;
    public GameObject tooltip;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GameObject UIRoot = GameObject.Find("Canvas").gameObject;
        tooltip = Instantiate(Resources.Load(string.Format("Prefabs/TooltipCanvas")), UIRoot.transform) as GameObject;
        var rrr = tooltip.GetComponent<TooltipController>();
        rrr.TooltipText.text = TooltipText;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Destroy(tooltip);
    }
}
