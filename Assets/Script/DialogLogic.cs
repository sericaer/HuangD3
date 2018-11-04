using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{
    public static event Action evntDestory;

	// Use this for initialization
	void Start ()
    {
        var buttons = this.GetComponentsInChildren<Button>();
        foreach(var btn in buttons)
        {
            btn.onClick.AddListener(() =>
            {
                Destroy(this.gameObject);
                evntDestory();
            });
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static GameObject newDialogInstace(string title, string content, List<Tuple<string, Action>> options)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
        GameObject dialog = Instantiate(Resources.Load(string.Format("Prefabs/DialogEvent")), UIRoot.transform) as GameObject;

        Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
        Text txContent = dialog.transform.Find("content").GetComponent<Text>();

        txTitle.text = title;
        txContent.text = content;

        foreach (var opt in options)
        {
            var uiOption = CreateOption(opt.Item1);
            uiOption.transform.SetParent(dialog.transform.Find("Panel"));

            uiOption.GetComponent<Button>().onClick.AddListener(() =>
            {
                opt.Item2();
            });

        }
  
        dialog.transform.SetAsLastSibling();
        return dialog;
	}

    public static GameObject CreateOption(string optname)
    {
        GameObject button = new GameObject("button", typeof(RectTransform));
        button.AddComponent<Button>();
        button.AddComponent<CanvasRenderer>();
        button.AddComponent<Image>();
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 30);
        
        GameObject Text = new GameObject("Text", typeof(RectTransform));
        Text.AddComponent<CanvasRenderer>();
        var text = Text.AddComponent<Text>();
        text.text = optname;
        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;
        Text.transform.SetParent(button.transform);

        return button;
    }
}
