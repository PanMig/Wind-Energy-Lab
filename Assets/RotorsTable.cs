using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotorsTable : MonoBehaviour {

    public Sprite[] sprites;
    private Image rotorImage;
	// Use this for initialization
	void Start () {

        rotorImage = gameObject.GetComponent<Image>();

	    if(LocalizationService.Instance.Localization == "Greek")
        {
            rotorImage.sprite = sprites[0];
        }
        else if (LocalizationService.Instance.Localization == "English")
        {
            rotorImage.sprite = sprites[1];
        }
        else
        {
            rotorImage.sprite = sprites[1];
        }
    }
}
