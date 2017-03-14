using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurbineInputManager : MonoBehaviour {
    public bool turbineIsClicked = false;
	public Canvas PopUpCanvas;
	private Text popUpText;
	private Image backgroundImage;
	private bool displayPopUpText = false;
	private TurbineAnimCtrl turbine;
	private HighlightObject bodyOutliner;

    void Start(){
		turbine = GetComponentInChildren<TurbineAnimCtrl>();
		bodyOutliner = GetComponentInChildren<HighlightObject>();
		InitializePopUpText();	
	}
	
	void Update(){
		DisplayPopUpText();
		bodyOutliner.ChangeMatColor(displayPopUpText);
	}

	void OnMouseDown () {
		turbineIsClicked = true;
	}

	void OnMouseOver(){
		displayPopUpText = true;
	}

	void OnMouseExit(){
		displayPopUpText = false;
	}

	void InitializePopUpText(){
		popUpText = PopUpCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Text>();
		backgroundImage = PopUpCanvas.GetComponentInChildren<Image>();
		popUpText.enabled = false;
		backgroundImage.enabled = false;
	}

	void DisplayPopUpText(){
		if(displayPopUpText == true){
			if(turbine.isRotating == true){
				popUpText.color = Color.red;
				popUpText.text = "Turn off";
			}
			else {
				popUpText.color = Color.blue;
				popUpText.text = "Turn on";
				
			}
			popUpText.enabled = true;
			backgroundImage.enabled = true;
		}
		else{
			popUpText.enabled = false;
			backgroundImage.enabled = false;
		} 
	}

}
