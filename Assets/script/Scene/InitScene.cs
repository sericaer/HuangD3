using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitScene : MonoBehaviour
{
    public static string nameDynasty { get; set; }

    public static string nameYear { get; set; }

    public static string nameEmperor { get; set; }

    void Awake()
    {
        GameObject.Find("Canvas/Panel/BtnConfirm").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadSceneAsync("MainScene");
        });

        inputDynastyName = GameObject.Find("Canvas/Panel/DynastyName/InputField").GetComponent<InputField>();
        inputYearName    = GameObject.Find("Canvas/Panel/YearName/InputField").GetComponent<InputField>();
        inputEmperorName = GameObject.Find("Canvas/Panel/EmperorName/InputField").GetComponent<InputField>();

        inputDynastyName.onEndEdit.AddListener((string name) => { nameDynasty = name; });
        inputYearName.onEndEdit.AddListener((string name) => { nameYear = name; });
        inputEmperorName.onEndEdit.AddListener((string name) => { nameEmperor = name; });

        inputDynastyName.text = nameDynasty;
        inputYearName.text = nameYear;
        inputEmperorName.text = nameEmperor;

    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    private InputField inputDynastyName;
    private InputField inputYearName;
    private InputField inputEmperorName;

}
