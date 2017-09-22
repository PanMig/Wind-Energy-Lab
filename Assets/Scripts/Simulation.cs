using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour {
	[Header ("Text fields")]
	public Text windText;
	public Text timeText;
	public Text powerReqText;
	public Text powerOutputText;
	public Text powerUsageText;
	public Text incomeText;

	[Space]
	[Header ("Action added Text")] // the text that is displayed for 2 seconds(upon the minimap) when interacting with the turbine.

	//the side text next to the panel of the output power.
	public Text powerOutputSideText;
	public Image powerOutputsideImage;

	[Space]
	[Header ("Simulation variables")]

	/* =====================================
			wind simulation fields
	======================================*/
	public int windMinSpeed = 4;
	public int windMaxSpeed = 16;
	public int currentWindSpeed;
	// 0 = wind speed is decreasing, 1 = is increasing.
	private int windChangeDirection = 0; 
	private int windChangeCounter = 2;
	public Image windIncreaseIcon;
	public Image windDecreaseIcon;
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
	 
	/*=====================================
		power Reqs simulation fields
	======================================*/
	public int powerRequirementsMin = 1000;
	public int powerRequirementsMax = 24000;
	private int	currentPowerReqs;
	private int	powerChangeDirection = 0;
	private int	powerChangeCounter = 2;
    private int singleTurbinePower = 0;
	/* =====================================
		power Output simulation fields
	======================================*/
	private int[] singlePowerOutput = {0,0,50,100,200,400,700,1000,1500,2000,2300,2400,2450,2500,2500,2500,2500,2500,2500,2500,2500};
    private float totalPowerOutput;
	public TurbineSpawnManager spawnManager;
	public string powerUsage = "-Under power" ; //TODO : maybe this can be changed to a enum, but it will less readable to the next developer that gets the source code.
    public float income = 8;

	// The colors for the power usage text.
	private Color red;
	private Color green;
	private Color blue;

    void Start(){
		powerOutputSideText.enabled = false;
		powerOutputsideImage.enabled = false;
		//the icons that display wind change
		windIncreaseIcon.enabled = false;
		windDecreaseIcon.enabled = false;
		//initialize values to random prices.
		currentPowerReqs = Random.Range(10200,15200);
		currentWindSpeed = Random.Range(10,12);
		startTime = Time.time;
		//initialize color(used as text colors)
		red = new Color(2,0,0,1);
		green = new Color(0,118,0,255);
		blue = Color.blue;
	}

	void Awake() {
		float firstExecution = 0.0f;
		float repeatRate = 15.0f;
		//call methods to simulate simulation values (wind , power reqs, income).
		InvokeRepeating("CalculateWindSpeed",firstExecution,repeatRate);
		InvokeRepeating("CalculatePowerRequirements",firstExecution,30.0f);
		InvokeRepeating("incomeCalculation",firstExecution,60.0f);
	}
	
	
	// Update is called once per frame
	void Update () {
		DisplayText("income");
		CalculateTime();
	}

	//it is not called every frame, but every fixed frame (helps performance).
	void FixedUpdate(){
		/* 
		Upates the power produced and the power usage text.
		This values are updated every fixed frame, because there in no
		fixed time when their values will change (e.g wind changes, power reqs for fixed amount of time).		
		*/
		calculateOutputPower();
		CalculatePowerUsage();
	}

	/* 
	=====================================
			Calculate Time flow
	=====================================
	*/
	void CalculateTime(){
		Time.timeScale = simulationSpeed;
		time = Time.time - startTime ;
		minutes = ((int) (time/60)).ToString();
		minutesCount = ((int) (time/60));
		seconds = (time%60);
		secondstr = ((int)seconds).ToString();
		timeText.text = minutes + ":" +secondstr ;
	}


	/* 
	=====================================
			Wind speed calculation
	=====================================
	*/
	void CalculateWindSpeed(){
		
		int windAdjust;
		if(windChangeDirection == 0){
			// display decrease icon 
			windIncreaseIcon.enabled = false;
			windDecreaseIcon.enabled = true;
			windAdjust = Mathf.FloorToInt(Mathf.Floor(Random.Range(-3.0f,0.0f)));
			if((currentWindSpeed + windAdjust) >= windMinSpeed){
				currentWindSpeed += windAdjust;
			}
			windChangeCounter--;
			if(windChangeCounter == 0){
				windChangeCounter = Mathf.FloorToInt(Mathf.Floor(Random.Range(2.0f,5.0f)));
				windChangeDirection = 1;
			}
		}
		 else{
			// display increase icon
			windDecreaseIcon.enabled = false;
			windIncreaseIcon.enabled = true;
			windAdjust = Mathf.FloorToInt(Mathf.Floor(Random.Range(1.0f,4.0f)));
			if((currentWindSpeed+windAdjust) <= windMaxSpeed){
				currentWindSpeed += windAdjust;	
			}
			windChangeCounter--;
			if(windChangeCounter == 0){
				windChangeCounter = Mathf.FloorToInt(Mathf.Floor(Random.Range(2.0f,5.0f)));
				windChangeDirection = 0;
			}		
		}
		CalculateBarriers("wind");
		DisplayText("wind");	
	}


	/* 
	=====================================
		Power requirements calculation
	=====================================
	*/

	void CalculatePowerRequirements(){
		int powerAdjust;
		int powerAdjustMultiplier = (Mathf.FloorToInt(Random.Range(5.0f,8.0f)))*100;	
		int powerDirectionAmount = Mathf.FloorToInt(Random.Range(6.0f,9.0f));
		
		if( powerChangeDirection == 0 ){
			// decrease
			powerAdjust = Mathf.FloorToInt(Random.Range(-3.0f,0.0f))*powerAdjustMultiplier;
			if((currentPowerReqs + powerAdjust) >= powerRequirementsMin){
				currentPowerReqs += powerAdjust;
			}
			powerChangeCounter--;
			if(powerChangeCounter == 0){
				powerDirectionAmount = Mathf.FloorToInt(Random.Range(3.0f,6.0f));
				powerChangeCounter = Mathf.FloorToInt(Random.Range(powerDirectionAmount,powerDirectionAmount + 3.0f));
				powerChangeDirection = 1;
			}
		}
		else{
			// increase
			powerAdjust = (Mathf.FloorToInt(Random.Range(1.0f,4.0f))) * powerAdjustMultiplier;
			if((currentPowerReqs + powerAdjust) <= powerRequirementsMax){
				currentPowerReqs += powerAdjust;
			}
			powerChangeCounter--;
			if( powerChangeCounter == 0 ){
				powerDirectionAmount = Mathf.FloorToInt(Random.Range(3.0f,6.0f));
				powerChangeCounter = Mathf.FloorToInt(Random.Range(powerDirectionAmount,powerDirectionAmount + 3.0f));
				powerChangeDirection = 0;
			}
		}
		CalculateBarriers("powerReqs");
		DisplayText("powerReqs");

	}


	/* 
	=====================================
		Calculate power Output
	=====================================
	*/
	void calculateOutputPower(){
		singleTurbinePower = singlePowerOutput[currentWindSpeed];
		totalPowerOutput = (singleTurbinePower * spawnManager.numberOfTurbinesOperating )* 0.001f;
		DisplayText("powerOutput");
	}

	/* displays the added power output to the total amount 
	that each turbine is producing (text above the power output)*/
    public IEnumerator calculateAddedPower()
    {
		int addedAmount =  singlePowerOutput[currentWindSpeed];
		powerOutputSideText.text = " + " + addedAmount.ToString();
		powerOutputSideText.enabled = true;
		powerOutputsideImage.enabled = true;
		yield return new WaitForSeconds(2f);
		powerOutputSideText.enabled = false;
		powerOutputsideImage.enabled = false;
    }
	
	public IEnumerator calculateSubstractedPower()
    {
		int substractedAmount =  singlePowerOutput[currentWindSpeed];
		powerOutputSideText.text = " - " + substractedAmount.ToString();
		powerOutputSideText.enabled = true;
		powerOutputsideImage.enabled = true;
		yield return new WaitForSeconds(2f);
		powerOutputSideText.enabled = false;
		powerOutputsideImage.enabled = false;
    }

    /* 
	=====================================
			Calculate income
	=====================================
	*/
    void incomeCalculation(){
		if(string.Equals(powerUsage,"-Under power")){
			income += 0.5f;
		}
		else if(string.Equals(powerUsage,"-Correct power")){
			income += 1.5f;
		}
		DisplayText("income");
	}

	/* 
	=====================================
		Calculate power Usage
	=====================================
	*/
	void CalculatePowerUsage(){
		calculateOutputPower();
		int localpowerDiff = (int)totalPowerOutput - currentPowerReqs;
		if (localpowerDiff < 0){
			powerUsage = "-Under power";
			powerUsageText.color = red;
		}
		else if((totalPowerOutput - currentPowerReqs) > singleTurbinePower){
			powerUsage = "-Over power";
			powerUsageText.color = blue;
		}
		else {
			powerUsage = "-Correct power";
			powerUsageText.color = green;
		}
		DisplayText("powerUsage");
	}

	/* 
	===========================================
		Display text based on the given string
	===========================================
	*/
	void DisplayText(string whatTypeToDisplay){
		
		if(string.Equals(whatTypeToDisplay,"wind")){
			windText.text = currentWindSpeed.ToString();
		}
		else if(string.Equals(whatTypeToDisplay,"powerOutput")){
			powerOutputText.text = totalPowerOutput.ToString();
		}
		else if(string.Equals(whatTypeToDisplay,"powerReqs")){
			powerReqText.text = currentPowerReqs.ToString();
		}
		else if(string.Equals(whatTypeToDisplay,"powerUsage")){
			powerUsageText.text = powerUsage;
		}
		else if(string.Equals(whatTypeToDisplay,"income")){
			incomeText.text = income.ToString();
		}
		else{
			Debug.Log("wrong input at DisplayText() , check given parameters");
		}

	}


	/*
	=================================================
				Barriers calculation
	Calculate if simulated values go beyond 
	or above their declared barriers(max - min values)
	=================================================
	*/
	void CalculateBarriers(string whatTypeForBarrierCheck){
		//wind	
		if(string.Equals(whatTypeForBarrierCheck,"wind")){
			if(currentWindSpeed > windMaxSpeed){
				currentWindSpeed = windMaxSpeed;
			}
			else if(currentWindSpeed < windMinSpeed){
				currentWindSpeed = windMinSpeed;
			}
		}
		// power Reqs
		else if(string.Equals(whatTypeForBarrierCheck,"powerReqs")){
			
			if(currentPowerReqs > powerRequirementsMax){
				currentPowerReqs = powerRequirementsMax;
			}
			else if(currentPowerReqs < powerRequirementsMin){
				currentPowerReqs = powerRequirementsMin;
			}
		}
		else{
			Debug.Log("wrong input at CalculateBarriers() , check given parameters");
		}
	}

}