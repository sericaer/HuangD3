using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GMDATA;

public class EmperorLogic : MonoBehaviour
{
    private void Awake()
    {
        _uiEmperorName = this.transform.Find("emperorName/value").GetComponent<Text>();
        _uiEmperorAge = this.transform.Find("emperorDetail/age/value").GetComponent<Text>();
        _uiEmperorHeath = this.transform.Find("emperorDetail/heath/slider").GetComponent<Slider>();

        var uiEmperorDetail = this.transform.Find("emperorDetail").gameObject;
        {
            uiEmperorDetail.SetActive(false);
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                uiEmperorDetail.SetActive(!uiEmperorDetail.activeSelf);
                CountryStatusLogic.inst.gameObject.SetActive(!CountryStatusLogic.inst.gameObject.activeSelf);
            });
        }
    }
    // Use this for initialization
    void Start ()
    {
        _uiEmperorName.text = GMData.Inist.emperor.name;
        _uiEmperorAge.text = GMData.Inist.emperor.age.ToString();
        _uiEmperorHeath.value = GMData.Inist.emperor.heath;

    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    Text _uiEmperorName;
    Text _uiEmperorAge;
    Slider _uiEmperorHeath;
}
