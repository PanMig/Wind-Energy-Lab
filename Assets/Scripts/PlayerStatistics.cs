using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatistics : MonoBehaviour {

	private Simulation simulator;
	public static int underPowerSec;
    public static int correctPowerSec;
    public static int overPowerSec;
    public bool endSimulation = false;

    // Use this for initialization
    void Start () {
		endSimulation = false;
		if(SceneManager.GetActiveScene().buildIndex == 1){
			simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
			InitializeCountValues();
			InvokeRepeating("CalculatePowerUsageStatistics",60.0f,1.0f);
		}
	}

	void Update(){
		EndSimulation();
	}

	void InitializeCountValues(){
		underPowerSec = 0;
		correctPowerSec = 0;
		overPowerSec = 0;
	}

	public void EndSimulation(){
		if(SceneManager.GetActiveScene().buildIndex == 1){
			if(simulator.minutesCount >= 24 || endSimulation == true){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				Resources.UnloadUnusedAssets();
			}
		}
	}

	public void ExitButtonPressed(){
		endSimulation = true;
	}
	
	 void CalculatePowerUsageStatistics(){
		if(SceneManager.GetActiveScene().buildIndex == 1){
			if(string.Equals(simulator.powerUsage,"-Under power")){
				underPowerSec++;
			}
			else if(string.Equals(simulator.powerUsage,"-Correct power")){
				correctPowerSec++;
			}
			else {
				overPowerSec++;
			}
		}
	}

}
