using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatistics : MonoBehaviour {

	private Simulation simulator;
	public static int underPowerSec;
    public static int correctPowerSec;
    public static int overPowerSec;

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2)
        {
            simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
            InitializeCountValues();
            InvokeRepeating("CalculatePowerUsageStatistics",0.0f, 1.0f);
        }
	}

	void InitializeCountValues() {
        underPowerSec = 0;
		correctPowerSec = 0;
        overPowerSec = 0;
    }

	/*
	It holds to static variables the seconds that the player has spent in each power output scenario respectively.
	These values are later used in the end scene to calculate and display the usage of the wind farm, concerning the time spent in each scenario. 
	*/
 	void CalculatePowerUsageStatistics(){
        if (string.Equals(simulator.powerUsage, "-Under power"))
        {
            underPowerSec++;
        }
        else if (string.Equals(simulator.powerUsage, "-Correct power"))
        {
            correctPowerSec++;
        }
        else
        {
            overPowerSec++;
        }
	}

}
