using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour {

	public Text windText;
	public Text timeText;
	public Text powerReqText;
	public Text powerOutputText;
	public Text powerUsageText;
	
	/* =====================================
			wind simulation fields
	======================================*/
	private int windMinSpeed = 4;
	private int windMaxSpeed = 16;
	private int currentWindSpeed;
	// 0 = wind speed is decreasing, 1 = is increasing.
	private int windChangeDirection = 0; 
	private int windChangeCounter = 2;
	 /* =====================================
			time simulation fields
	======================================*/
	private float startTime;
	private float seconds;
	private int simulationSpeed = 90;
	 
	/*=====================================
			power Reqs simulation fields
	======================================*/
	public int	powerRequirementsMin = 20000;
	public int	powerRequirementsMax = 24000;
	public int	currentPowerReqs = 15000;
	public int	powerChangeDirection = 0;
	public int	powerChangeCounter = 2;
    private int singleTurbinePower = 0;
	
	/* =====================================
			power Output simulation fields
	======================================*/
	private int[] singlePowerOutput = {0,0,50,100,200,400,700,1000,1500,2000,2300,2400,2450,2500,2500,2500,2500,2500,2500,2500};
    private int totalPowerOutput;
	public SpawnObjectManager spawnManager;
	private string powerUsage = "Under power" ;

    void Start(){
		startTime = Time.time;
		currentWindSpeed = 10;
	}

	void Awake() {
		float firstExecution = 0.0f;
		float repeatRate = 5.0f;
		InvokeRepeating("CalculateWindSpeed",firstExecution,repeatRate);
		InvokeRepeating("CalculatePowerRequirements",firstExecution,10.0f);
		InvokeRepeating("CalculatePowerUsage",firstExecution,repeatRate);
	}
	
	
	// Update is called once per frame
	void Update () {
		CalculateTime();
	}

	/* 
	=====================================
			Calculate Time flow
	=====================================
	*/
	void CalculateTime(){
		Time.timeScale = simulationSpeed;
		float time = Time.time - startTime ;
		string minutes = ((int) (time/60)).ToString();
		seconds = (time%60);
		string secondstr = ((int)seconds).ToString();
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
			// decrease
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
			// increase
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
		calculateOutputPower();
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
		calculateOutputPower();
		DisplayText("powerReqs");

	}


	/* 
	=====================================
		Calculate power Output
	=====================================
	*/
	void calculateOutputPower(){
		singleTurbinePower = singlePowerOutput[currentWindSpeed];
		totalPowerOutput = singleTurbinePower * spawnManager.numberOfTurbinesOperating;
		DisplayText("powerOutput");
	}


	/* 
	=====================================
		Calculate power Usage
	=====================================
	*/
	void CalculatePowerUsage(){
		calculateOutputPower();
		int localpowerDiff = totalPowerOutput - currentPowerReqs;
		if (localpowerDiff < 0){
			powerUsage = "Under power ";
			powerUsageText.color = new Color(2,0,0,1);
		}
		else if((totalPowerOutput - currentPowerReqs) > singleTurbinePower){
			powerUsage = "Over power";
			powerUsageText.color = new Color(0,0,1,255);
		}
		else {
			powerUsage = "Correct power";
			powerUsageText.color = new Color(0,-2,0,255);
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
			windText.text = "Wind : " + currentWindSpeed.ToString() + " m/s";
		}
		else if(string.Equals(whatTypeToDisplay,"powerOutput")){
			powerOutputText.text = "Power Output : " + totalPowerOutput.ToString() + " Kw";
		}
		else if(string.Equals(whatTypeToDisplay,"powerReqs")){
			powerReqText.text = "Power Reqs : " + currentPowerReqs.ToString() + " Kw";
		}
		else if(string.Equals(whatTypeToDisplay,"powerUsage")){
			powerUsageText.text ="- " + powerUsage;
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



/* time calculation
 float t = Time.time;
 int sec = (int)(t%60);
 int minutes = (int)((t/60)%60)
 int hours = (int)((t/3600)%24)
 int days = (int)(t/86400); // There are 86400 seconds in a day (60*60*24)

 */