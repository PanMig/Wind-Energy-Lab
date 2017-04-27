using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {
	
	public bool gamePaused = false;
	public Simulation simulator;
    private int prevSimulationSpeed;
	public Text text;


    void Start () {
		text.enabled = false;
		prevSimulationSpeed = simulator.simulationSpeed;
	}


	public void PauseButtonPressed(){
		if(gamePaused == false){
			gamePaused = true;
			prevSimulationSpeed = simulator.simulationSpeed;
			simulator.simulationSpeed = 0;
			text.enabled = true;	
		}
		else if(gamePaused == true){
			gamePaused = false;
			simulator.simulationSpeed = prevSimulationSpeed;
			text.enabled = false;
		}
	}
}
