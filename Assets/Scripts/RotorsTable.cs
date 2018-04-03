using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotorsTable : MonoBehaviour {

    public Sprite ENG;
    public Sprite GRE;

    // Use this for initialization
    void Start()
    {
        if (LocalizationService.Instance.Localization == "English")
        {
            gameObject.GetComponent<Image>().sprite = ENG;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = GRE;
        }
    }

}
