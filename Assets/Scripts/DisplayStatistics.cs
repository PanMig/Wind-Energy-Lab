using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatistics : MonoBehaviour {

	public Text usage;
	public Text Cost;
	public Text Profit;
    private int underPowerMin;
    private int correctPowerMin;
    private int overPowerMin;
    float underPowerPercent;
    float correctPowerPercent;
    float overPowerPercent;

    // Use this for initialization
    void Start () {
		DisplayUsage();
		DisplayCost();
        DisplayProfit();

    }

    private void DisplayCost()
    {
        String cost = GameManager.cost.ToString();
        Cost.text = "General cost is : " + cost + " $";
    }

    void ConvertSecondToMin()
    {
		underPowerMin = PlayerStatistics.underPowerSec/60;
		correctPowerMin = PlayerStatistics.correctPowerSec/60;
		overPowerMin = PlayerStatistics.overPowerSec/60;
	}
	
	void DisplayUsage()
    {
        float sumOfTime = PlayerStatistics.underPowerSec + PlayerStatistics.correctPowerSec + PlayerStatistics.overPowerSec;
        underPowerPercent = (PlayerStatistics.underPowerSec / sumOfTime) * 100.0f;
        correctPowerPercent = (PlayerStatistics.correctPowerSec / sumOfTime) * 100.0f;
        overPowerPercent = (PlayerStatistics.overPowerSec / sumOfTime) * 100.0f;
        usage.text = "Under power : " + underPowerPercent.ToString("F2") + " % " + "\n" +
                     "Correct power : " + correctPowerPercent.ToString("F2") + " % " + "\n" +
                     "Over power : " + overPowerPercent.ToString("F2") + " % ";
    }

    void DisplayProfit()
    {
        Profit.text = "You could sell the excess of power your Wind Farm generated and earn  " + (PlayerStatistics.profitAmount * 0.1f).ToString() + " $ "+
            "per year of operation."; 
    }

}
