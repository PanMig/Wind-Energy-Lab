using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurbineInputManager : MonoBehaviour {
    
	public Canvas PopUpCanvas;
	private Text popUpText;
	private Image backgroundImage;
	//used to highlight the turbine with a color on mouse over
	private HighlightObject outliner;
    private TurbineController turbine;
	private bool displayPopUpText = false;
	private Vector2 mousePos;
	private Vector2 canvasPos;

    void Start(){
		turbine = GetComponent<TurbineController>();
		outliner = GetComponentInChildren<HighlightObject>();
		InitializePopUpText();
	}

	void Update(){
		//used for highlighting the turbine when is damaged
		if(turbine.IsDamaged()){
			HighlightTurbine(turbine.IsDamaged());
		}
	}

	void OnMouseDown () {
		if(turbine.IsRotating() == true && turbine.IsDamaged() == false){
			turbine.DisableTurbine();
		}
		else if(turbine.IsRotating() == false && turbine.IsDamaged() == false){
			turbine.EnableTurbine();
		}
		else if(turbine.IsDamaged() == true){
			turbine.repairTurbine();
		}
	}

	void HighlightTurbine(bool isDamaged){
		if(isDamaged == false){
			outliner.SetUnDamagedMat(displayPopUpText);	
		}
		else{
			outliner.SetDamagedMat(displayPopUpText);
		}
	}

	void OnMouseEnter(){
		PlaceCanvasToMouse();
		displayPopUpText = true;
		DisplayPopUpText();
		HighlightTurbine(turbine.IsDamaged());
	}

	void OnMouseExit(){
		displayPopUpText = false;
		DisplayPopUpText();
		HighlightTurbine(turbine.IsDamaged());
	}


	void InitializePopUpText(){
		popUpText = PopUpCanvas.GetComponentInChildren<Image>().GetComponentInChildren<Text>();
		backgroundImage = PopUpCanvas.GetComponentInChildren<Image>();
		popUpText.enabled = false;
		backgroundImage.enabled = false;
	}

	void DisplayPopUpText(){
		if(displayPopUpText == true){
			if(turbine.IsRotating() == true){
				popUpText.color = Color.red;
				popUpText.text = "Turn off";
			}
			else if(turbine.IsRotating() == false && turbine.IsDamaged() == false){
				popUpText.color = Color.blue;
				popUpText.text = "Turn on";
				
			}
			else {
				popUpText.text = "repair";
				popUpText.color = Color.black;
			}
			popUpText.enabled = true;
			backgroundImage.enabled = true;
		}
		else{
			popUpText.enabled = false;
			backgroundImage.enabled = false;
		} 
	}

	void PlaceCanvasToMouse(){
		mousePos = Input.mousePosition;
		canvasPos.x = mousePos.x;
		canvasPos.y = mousePos.y+20;
		PopUpCanvas.GetComponentInChildren<Image>().transform.position = canvasPos;
	}

}
