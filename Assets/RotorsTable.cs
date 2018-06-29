using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotorsTable : MonoBehaviour {

    public Sprite[] sprites;
    public Image rotorImage;

	// Use this for initialization
	void Start () {

        rotorImage = gameObject.GetComponent<Image>();

	    if(GameManager.instance.Windclass == 1)
        {
            rotorImage.sprite = sprites[0];
        }
        else if (GameManager.instance.Windclass == 2)
        {
            rotorImage.sprite = sprites[1];
        }
        else
        {
            rotorImage.sprite = sprites[2];
        }
    }
}
