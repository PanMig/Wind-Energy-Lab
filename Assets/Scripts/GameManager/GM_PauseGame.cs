using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM_PauseGame : MonoBehaviour
{

    public bool gamePaused = false;
    private Simulation simulator;
    private int prevSimulationSpeed;
    public Text pauseText;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2)
        {
            simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
            pauseText.enabled = false;
            prevSimulationSpeed = simulator.simulationSpeed;
        }
    }


    public void PauseButtonPressed()
    {
        if (gamePaused == false)
        {
            gamePaused = true;
            prevSimulationSpeed = simulator.simulationSpeed;
            simulator.simulationSpeed = 0;
            pauseText.enabled = true;
        }
        else if (gamePaused == true)
        {
            gamePaused = false;
            simulator.simulationSpeed = prevSimulationSpeed;
            pauseText.enabled = false;
        }
    }

}
