using UnityEngine;
using UnityEngine.UI;

public class PauseSimulation : MonoBehaviour
{

    public bool gamePaused = false;
    private int prevSimulationSpeed;
    private Text pauseText;
    private Simulation simulator;

    void Start()
    {
        simulator = GetComponent<Simulation>();
        pauseText = GameObject.FindGameObjectWithTag("PauseText").GetComponent<Text>();
        pauseText.enabled = false;
        prevSimulationSpeed = simulator.simulationSpeed;  
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
