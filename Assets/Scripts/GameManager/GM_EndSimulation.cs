using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_EndSimulation : MonoBehaviour {

    public bool endSimulation = false;
    public float simulationDurationTime;
    private int sceneCount;
    private Simulation simulator;

    private bool valuesInitialized = false;

    // Use this for initialization
    void Start () {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        endSimulation = false;
    }

    void Update()
    {
        //simumlation scene
        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 2 && valuesInitialized == false)
        {
            simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
            valuesInitialized = true;
        }
        EndSimulation();
    }


    /*
	Stops the simulation and loads the end scene in the game. This can be achieved either by clicking on the exit button,
	or after 24 minutes have passed.
	*/
    public void EndSimulation()
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 2) // if the pre-last scene
        {
            if (simulator.minutesCount >= simulationDurationTime || endSimulation == true)
            {
                SceneManager.LoadScene(sceneCount - 1); // loads last scene
                Resources.UnloadUnusedAssets(); //removes unused assets to free memory
            }
        }
    }


    public void ExitButtonPressed()
    {
        endSimulation = true;
    }
}
