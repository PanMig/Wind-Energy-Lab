using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTowerLight : MonoBehaviour {
	[SerializeField] private Light underPowerLight;
	[SerializeField] private Light overPowerLight;
	[SerializeField] private Light correctPowerLight;
	[SerializeField] private Simulation simulator;
	
	void start(){
		underPowerLight.enabled = true;
		overPowerLight.enabled = false;
		correctPowerLight.enabled = false;
	}
	void Update () {
		enableLight();
	}

	private void enableLight(){
		if(string.Equals(simulator.powerUsage,"-Under power")){
			underPowerLight.enabled = true;
			overPowerLight.enabled = false;
			correctPowerLight.enabled = false;	
		}
		else if(string.Equals(simulator.powerUsage, "-Over power")){
			underPowerLight.enabled = false;
			overPowerLight.enabled = true;
			correctPowerLight.enabled = false;
		}
		else {
			underPowerLight.enabled = false;
			overPowerLight.enabled = false;
			correctPowerLight.enabled = true;
		}
	}
	
}
