using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayStatistics : MonoBehaviour {

	public Text usage;
	public Text Cost;
	public Text profit;
	public Text score;
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
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            DisplayScore();
        }
    }

    private void DisplayCost()
    {
        String cost = GameManager.cost.ToString();
        Cost.text = "Your Wind Farm costed : " + cost + " $";
    }
	
	void DisplayUsage()
    {
        float sumOfTime = GameManager.instance.underPowerSec + GameManager.instance.correctPowerSec + GameManager.instance.overPowerSec;
        underPowerPercent = (GameManager.instance.underPowerSec / sumOfTime) * 100.0f;
        correctPowerPercent = (GameManager.instance.correctPowerSec / sumOfTime) * 100.0f;
        overPowerPercent = (GameManager.instance.overPowerSec / sumOfTime) * 100.0f;
        usage.text = "Under power : " + underPowerPercent.ToString("F2") + " % " + "\n" +
                     "Correct power : " + correctPowerPercent.ToString("F2") + " % " + "\n" +
                     "Over power : " + overPowerPercent.ToString("F2") + " % ";
    }

    void DisplayProfit()
    {
        profit.text = "You could sell the excess of power your Wind Farm generated and earn  " + (GameManager.instance.profit * 0.1f).ToString("F2") + " $ "+
            "per year of operation."; 
    }

    void DisplayScore()
    {
        CalculateScore();
        GameManager.instance.score = GameManager.instance.score * 10;

        //display score
        score.text = "Your score mark is : " + GameManager.instance.score.ToString() + " %";
    }


    void CalculateScore()
    {
        // score based on usage
        if (GameManager.instance.underPowerSec > GameManager.instance.correctPowerSec && GameManager.instance.underPowerSec > GameManager.instance.overPowerSec)
        {
            GameManager.instance.score += 1;
        }
        else if (GameManager.instance.correctPowerSec > GameManager.instance.underPowerSec && GameManager.instance.correctPowerSec > GameManager.instance.overPowerSec)
        {
            GameManager.instance.score += 3;
        }
        else
        {
            GameManager.instance.score += 2;
        }

        // score based on cost
        switch (GameManager.instance.Areachoice)
        {
            case GameManager.MainArea.mountains:
                if (GameManager.instance.Windclass == 1) GameManager.instance.score += 1;
                if (GameManager.instance.areaInstallationCost <= 3) GameManager.instance.score += 5;
                else if (GameManager.instance.areaInstallationCost > 3 && GameManager.instance.areaInstallationCost <= 5) GameManager.instance.score += 3;
                else if (GameManager.instance.areaInstallationCost >= 3) GameManager.instance.score += 2;
                break;
            case (GameManager.MainArea.fields):
                if (GameManager.instance.Windclass == 2) GameManager.instance.score += 1;
                if (GameManager.instance.areaInstallationCost <= 2) GameManager.instance.score += 5;
                else if (GameManager.instance.areaInstallationCost > 2 && GameManager.instance.areaInstallationCost <= 4) GameManager.instance.score += 3;
                else if (GameManager.instance.areaInstallationCost > 4 && GameManager.instance.areaInstallationCost <= 6) GameManager.instance.score += 2;
                break;
            case (GameManager.MainArea.seashore):
                if (GameManager.instance.Windclass == 3) GameManager.instance.score += 1;
                if (GameManager.instance.areaInstallationCost <= 3) GameManager.instance.score += 5;
                else if (GameManager.instance.areaInstallationCost > 3 && GameManager.instance.areaInstallationCost <= 5) GameManager.instance.score += 3;
                else if (GameManager.instance.areaInstallationCost > 5 && GameManager.instance.areaInstallationCost <= 7) GameManager.instance.score += 2;
                break;

        }
    }

}
