using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulation : MonoBehaviour {

	public int windMinSpeed = 4;
	public int windMaxSpeed = 16;
	public int currentWindSpeed = 10;

	// 0 = wind speed is decreasing, 1 = is increasing.
	private int windChangeDirection = 0; 
	private int windChangeCounter = 2;



	public void CalculateWindSpeed(){
		
		int windAdjust;
		if(windChangeDirection == 0){
			//print("wind decrease");
			// decrease
			windAdjust = Mathf.FloorToInt(Random.Range(-3,3));
			if((currentWindSpeed + windAdjust) >= windMinSpeed){
				currentWindSpeed += windAdjust;
			}
			windChangeCounter--;
			if(windChangeCounter == 0){
				windChangeCounter = (int)Random.Range(2,3);
				windChangeDirection = 1;
			}
		}
		 else{
			// increase
			//print("wind increase");
			windAdjust = Mathf.FloorToInt(Random.Range(1,3));
			if((currentWindSpeed+windAdjust) <= windMaxSpeed){
				currentWindSpeed += windAdjust;	
			}
			windChangeCounter--;
			if(windChangeCounter == 0){
				windChangeCounter = Mathf.FloorToInt(Random.Range(2,3));
				windChangeDirection = 0;
			}		
		}
		WindSpeedBarierCheck();
		//recalculateWind();	
	}

	/*check if wind speed is above the max and min barriers */
	void WindSpeedBarierCheck(){
		if(currentWindSpeed > windMaxSpeed){
			currentWindSpeed = windMaxSpeed;
		}
		else if(currentWindSpeed < windMinSpeed){
			currentWindSpeed = windMinSpeed;
		}
	}

	public void recalculateWind(){
		InvokeRepeating("CalculateWindSpeed",0.0f,5.0f);
	}

}
