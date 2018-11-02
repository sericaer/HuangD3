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

    public static GameObject newDialogInstace(string title, string content, string[] options)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
        GameObject dialog = Instantiate(Resources.Load(string.Format("Prefabs/Dialog_{0}Btn", options.Length)), UIRoot.transform) as GameObject;

        Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
        Text txContent = dialog.transform.Find("content").GetComponent<Text>();

        txTitle.text = title;
        txContent.text = content;
  
        dialog.transform.SetAsLastSibling();

        for (int i = 0; i < options.Length; i++)
        {
            Transform tran = dialog.transform.Find(string.Format("option{0}", i + 1));
            if (tran == null)
            {
                tran = dialog.transform.Find(string.Format("Content/option{0}", i + 1));
            }

            Button optionTran = tran.GetComponent<Button>();
            Text txop = optionTran.transform.Find("Text").GetComponent<Text>();
            txop.text = options[i];
        }

        return dialog;
	}
}
