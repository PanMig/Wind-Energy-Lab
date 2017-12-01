using UnityEngine.UI;
using UnityEngine;
using goedle_sdk;

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
        GoedleAnalytics.track("press.uiButton", "configure Button");
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    public void SetsimulationSpeed(int index)
    {
        if (index == 0)
        {
            simulator.simulationSpeed = 1;
            GoedleAnalytics.track("press.uiButton", "normal speed button");
        }
        else if (index == 1)
        {
             simulator.simulationSpeed = 3;
            GoedleAnalytics.track("press.uiButton", "fast speed button");
        }
        else simulator.simulationSpeed = 1;
    }
}
