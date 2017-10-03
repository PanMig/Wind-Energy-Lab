using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Simulation : MonoBehaviour {
	[Header ("Text fields")]
	public Text windText;
	public Text timeText;
	public Text powerReqText;
	public Text powerOutputText;
	public Text powerUsageText;

	[Space]
	[Header ("Action added Text")] // the text that is displayed for 2 seconds(upon the minimap) when interacting with the turbine.

	//the side text next to the panel of the output power.
	public Text  powerOutputSideText;
	public Image powerOutputsideImage;

	[Space]
	[Header ("Simulation variables")]

	public int currentWindSpeed;
    private int currentPowerReqs;

    /* =====================================
			time simulation fields
	======================================*/
    private float startTime;
	public float minutesCount;
	private float seconds;
	public int simulationSpeed = 2;
	private float time;
	private string minutes;
	private string secondstr;
    /* =====================================
		power Output simulation fields
	======================================*/
    private float turbineDefaultOutput;
    private float totalPowerOutput;
    //private int[] singleTurbineOutput = {0,0,0,0,0,1,2,3,4,5,6,6,6,6,6};
    private float[] singleTurbineOutput = new float[15];
    public TurbineSpawnManager spawnManager;

    public string powerUsage = "-Under power"; //TODO : maybe this can be changed to a enum, but it will less readable to the next developer that gets the source code.
    enum DisplayedTextValue { wind ,powerReqs,powerOutput,powerUsage}



    void Start(){
		powerOutputSideText.enabled = false;
		powerOutputsideImage.enabled = false;

        turbineDefaultOutput = TurbineSelector.turbineDefaultPower;
        CalculatePowerRequirements();
        currentWindSpeed = 10;
		startTime = Time.time;

        InitializeTurbineOutputArray();
	}

	void Awake() {
		float firstExecution = 0.0f;
		InvokeRepeating("CalculateWindSpeed",firstExecution,15.0f);
		//InvokeRepeating("CalculatePowerRequirements",firstExecution,30.0f);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateTime();
        EndSimulation();
	}

	//it is not called every frame, but every fixed frame (helps performance).
	void FixedUpdate(){
		CalculatePowerOutput();
		CalculatePowerUsage();
	}

    void InitializeTurbineOutputArray() {

        float j = 0;
        if (turbineDefaultOutput == 6) j = 1;
        else if (turbineDefaultOutput == 3) j = 0.5f;
        else if (turbineDefaultOutput == 0.9f) j = 0.25f;
        int i;
        for (i = 14; i > 0; i--)
        {
            if (i <= 9)
            {
                if (turbineDefaultOutput >= 6) { singleTurbineOutput[i] = turbineDefaultOutput - j; j++; }
                else if (turbineDefaultOutput == 3) { singleTurbineOutput[i] = turbineDefaultOutput - j; j += 0.5f; }
                else if (turbineDefaultOutput == 0.9f) { singleTurbineOutput[i] = turbineDefaultOutput - j; j += 0.25f; }
            }
            else {
                singleTurbineOutput[i] = turbineDefaultOutput;
            } 
            singleTurbineOutput[i] = Mathf.Clamp(singleTurbineOutput[i], 0,turbineDefaultOutput);
        }
    }

    #region Calculated Simulation values

    void CalculateTime(){
		Time.timeScale = simulationSpeed;
		time = Time.time - startTime ;
		minutes = ((int) (time/60)).ToString();
		minutesCount = ((int) (time/60));
		seconds = (time%60);
		secondstr = ((int)seconds).ToString();
		timeText.text = minutes + ":" +secondstr ;
	}

    void CalculateWindSpeed()
    {
        currentWindSpeed = RandomGaussianGenerator.GenerateNormalRandom(10.0f, 1.67f, 5, 15);
        DisplayText(DisplayedTextValue.wind);
    }

    void CalculatePowerRequirements()
    {
        currentPowerReqs = 6;
        DisplayText(DisplayedTextValue.powerReqs);
    }

    void CalculatePowerOutput()
    {
        totalPowerOutput = spawnManager.numberOfTurbinesOperating * singleTurbineOutput[currentWindSpeed];
        DisplayText(DisplayedTextValue.powerOutput);
    }


    void CalculatePowerUsage(){
		int localpowerDiff = (int)totalPowerOutput - currentPowerReqs;
		if (localpowerDiff < 0){
			powerUsage = "-Under power";
            powerUsageText.color = Color.red;
		}
		else if((totalPowerOutput - currentPowerReqs) > 0){
			powerUsage = "-Over power";
			powerUsageText.color = Color.blue;
		}
		else {
			powerUsage = "-Correct power";
			powerUsageText.color = Color.green;
		}
		DisplayText(DisplayedTextValue.powerUsage);
	}

    #endregion


    #region Added / substracted action power
    /* displays the added power output to the total amount 
	that each turbine is producing (text above the power output)*/
    public IEnumerator calculateAddedPower()
    {
        float addedAmount = singleTurbineOutput[currentWindSpeed];
        powerOutputSideText.text = " + " + addedAmount.ToString();
        powerOutputSideText.enabled = true;
        powerOutputsideImage.enabled = true;
        yield return new WaitForSeconds(2f);
        powerOutputSideText.enabled = false;
        powerOutputsideImage.enabled = false;
    }

    public IEnumerator calculateSubstractedPower()
    {
        float substractedAmount = singleTurbineOutput[currentWindSpeed];
        powerOutputSideText.text = " - " + substractedAmount.ToString();
        powerOutputSideText.enabled = true;
        powerOutputsideImage.enabled = true;
        yield return new WaitForSeconds(2f);
        powerOutputSideText.enabled = false;
        powerOutputsideImage.enabled = false;
    }

    #endregion


    #region DisplayText

    void DisplayText(DisplayedTextValue textValue)
    {

        if (textValue == DisplayedTextValue.wind)
        {
            windText.text = currentWindSpeed.ToString();
        }
        else if (textValue == DisplayedTextValue.powerOutput)
        {
            powerOutputText.text = totalPowerOutput.ToString();
        }
        else if (textValue == DisplayedTextValue.powerReqs)
        {
            powerReqText.text = currentPowerReqs.ToString();
        }
        else if (textValue == DisplayedTextValue.powerUsage)
        {
            powerUsageText.text = powerUsage;
        }
        else
        {
            Debug.Log("wrong input at DisplayText() , check given parameters");
        }
    }

    #endregion


    #region Control Simulation

    public void EndSimulation()
    {
        if (minutesCount >= GameManager.instance.simulationDurationTime || GameManager.instance.endGame == true)
        {
            minutesCount = 0;
            GameManager.instance.endGame = false;
            GameManager.instance.LoadNextLevel();
            Resources.UnloadUnusedAssets(); //removes unused assets to free memory
        }
    }

    #endregion

}