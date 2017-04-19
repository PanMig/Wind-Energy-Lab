using UnityEngine;
using UnityEngine.UI;

public class DisplayStatistics : MonoBehaviour {

	public Text underPowerUsageText;
	public Text correctPowerUsageText;
	public Text overPowerUsageText;
	public Text usage;
    private int underPowerMin;
    private int correctPowerMin;
    private int overPowerMin;
    private int underPowerSec;
    private int correctPowerSec;
    private int overPowerSec;

    // Use this for initialization
    void Start () {
		ConvertSecondToMin();
		DisplayPlayerStatistics();
	}

	void ConvertSecondToMin(){
		underPowerMin = PlayerStatistics.underPowerSec/60;
		underPowerSec = PlayerStatistics.underPowerSec%60;
		correctPowerMin = PlayerStatistics.correctPowerSec/60;
		correctPowerSec = PlayerStatistics.correctPowerSec%60;
		overPowerMin = PlayerStatistics.overPowerSec/60;
		overPowerSec = PlayerStatistics.overPowerSec%60;
	}
	
	void DisplayPlayerStatistics(){
		underPowerUsageText.text = "Under power :  " + underPowerMin.ToString() + " minutes and " + underPowerSec.ToString() + " seconds" ;
		correctPowerUsageText.text = "Correct power :  " + correctPowerMin.ToString() + " minutes and " + correctPowerSec.ToString() + " seconds" ;
		overPowerUsageText.text = "Over power :  " + overPowerMin.ToString() + " minutes and " + overPowerSec.ToString() + " seconds" ;

		// set text msg based on power usage.
		if (underPowerMin > correctPowerMin && underPowerMin > overPowerMin){
			usage.text = "The wind farm was mostly working in under Power, not very efficient.";		
		}
		else if (overPowerMin > correctPowerMin && overPowerMin > underPowerMin){
			usage.text = "The wind farm was mostly working in over Power, not very efficient.";	
		}
		else if (correctPowerMin > underPowerMin && correctPowerMin > overPowerMin){
			usage.text = "The wind farm was mostly working in correct Power, very efficient.";
		}
		else if (underPowerSec > correctPowerSec && underPowerSec > overPowerSec){
			usage.text = "The wind farm was mostly working in under Power, not very efficient.";		
		}
		else if(overPowerSec > correctPowerSec && overPowerSec > underPowerSec){
			usage.text = "The wind farm was mostly working in over Power, not very efficient.";	
		}
		else {
			usage.text = "The wind farm was mostly working in correct Power, very efficient.";
		}
	}

}
