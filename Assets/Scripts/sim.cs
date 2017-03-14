using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sim : MonoBehaviour {
	
	public Text windText;
	//public Text timeText;
	//public Text powerReqText;

	public WindSimulation wind;

	void Awake() {
		//wind = gameObject.GetComponent<WindSimulation>();
		wind.recalculateWind();
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		print(Time.timeScale);
	}
	
	// Update is called once per frame
	void Update () {
		DisplayText();
	}

	void DisplayText(){
		windText.text = "Wind : " + wind.currentWindSpeed.ToString() + " m/s";
	}
}
