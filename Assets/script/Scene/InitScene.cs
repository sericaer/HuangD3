using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitScene : MonoBehaviour
{
    void Awake()
    {
        inputDynastyName = GameObject.Find("Canvas/Panel/DynastyName/InputField").GetComponent<InputField>();
        inputYearName = GameObject.Find("Canvas/Panel/YearName/InputField").GetComponent<InputField>();
        inputEmperorName = GameObject.Find("Canvas/Panel/EmperorName/InputField").GetComponent<InputField>();

        RandomName();

        GameObject.Find("Canvas/Panel/BtnConfirm").GetComponent<Button>().onClick.AddListener(() => {

            SceneManager.LoadSceneAsync("MainScene");
        });

        GameObject.Find("Canvas/Panel/BtnRandom").GetComponent<Button>().onClick.AddListener(() => {
            RandomName();
        });

    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    void RandomName()
    {
        inputDynastyName.text = StreamManager.DynastyName.GetRandom();
        inputYearName.text = StreamManager.YearName.GetRandom();
        inputEmperorName.text = StreamManager.PersonName.GetRandomMale();
    }

    private InputField inputDynastyName;
    private InputField inputYearName;
    private InputField inputEmperorName;

}
