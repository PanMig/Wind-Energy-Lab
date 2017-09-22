using UnityEngine.UI;
using UnityEngine;

public class ConfigMenu : MonoBehaviour {

    [SerializeField] private Canvas canvas;
    [SerializeField] private Simulation simulator;

	// Use this for initialization
	void Start () {
        canvas.enabled = false;
	}
 
	public void EnableCanvas()
    {
        canvas.enabled = true;
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    public void SetsimulationSpeed(int index)
    {
        if (index == 0) simulator.simulationSpeed = 1;
        else if (index == 1) simulator.simulationSpeed = 3;
        else simulator.simulationSpeed = 4;
    }
}
