using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineController : MonoBehaviour {
	private TurbineAnimCtrl turbineAnim;
	private TurbineDamage turbineDmg;
    private TurbineSpawnManager turbineSpawner;
	private TurbineInputManager inputManager;
	private TurbineRepair repair;
	private Simulation simulator;
	public static int damagedTurbines = 0;
	private bool lowWindDisabled = false;
	private PauseGame gameManager;
	private bool scriptsEnabled = true;

    // Use this for initialization
    void Start () {
		inputManager = GetComponent<TurbineInputManager>();
		turbineAnim = GetComponentInChildren<TurbineAnimCtrl>();
		turbineDmg = GetComponent<TurbineDamage>();
		repair = GetComponent<TurbineRepair>();
		turbineSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<TurbineSpawnManager>();
		simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseGame>();
	}

	void Update(){
		//checks if game is paused or not
		if(gameManager.gamePaused == true){
			PauseTurbine(gameManager.gamePaused);		
		}
		else{
			if(scriptsEnabled == false) UnPauseTurbine(gameManager.gamePaused);
			
			turbineAnim.SetRotationSpeed(simulator.currentWindSpeed);


			//used for disables rotation when wind is low
			if(simulator.currentWindSpeed < 3 && IsRotating() == true){
				DisableOnWindLow();
			}
			if(simulator.currentWindSpeed > 3 && IsRotating() == false && lowWindDisabled == true){
				EnableOnWindHigh();
			}
		
		}


	}

	public void DisableOnWindLow(){
			DisableTurbine();
			lowWindDisabled = true;
	}

	/* 
	make turbines rotate again when wind is not below
	the low speed.
	*/
	public void EnableOnWindHigh(){
			EnableTurbine();
			lowWindDisabled = false;
	}

	public bool IsRotating(){
		return turbineAnim.isRotating; 	
	}

	public bool IsDamaged(){
		return turbineDmg.isDamaged;
	}

	public void setDamage( bool isDamaged){
		turbineDmg.isDamaged = isDamaged;
	}

	public void repairTurbine(){
		if(simulator.income - 1 >= 0){
			simulator.income--;
			repair.turbineRepair();
			damagedTurbines--;
		}
	}

	public bool isRepaired(){
	 	return repair.isRepaired;
	}
	public void setRepair(bool repairBool){
		repair.isRepaired = repairBool;
	}

	public void DisableTurbine(){
		StartCoroutine(simulator.calculateSubstractedPower());
		turbineAnim.DisableRotation();
		turbineSpawner.numberOfTurbinesOperating--;	
	}

	public void EnableTurbine(){
		StartCoroutine(simulator.calculateAddedPower());
		turbineAnim.EnableRotation();
		turbineSpawner.numberOfTurbinesOperating++;
	}

	public int getTotalNumberOfTurbines(){
		return turbineSpawner.numberOfTurbines;	
	}

	public int getNumberOfTurbinesOperating(){
		return turbineSpawner.numberOfTurbinesOperating;	
	}

	//disables all scripts if game is paused
	public  void PauseTurbine(bool gamePaused){
		if(gamePaused == true){
			//disable animation
			turbineAnim.enabled = false;
			turbineAnim.GetComponent<Animator>().enabled = false;

			turbineDmg.enabled = false;
			repair.enabled = false;
			turbineSpawner.enabled = false;
			inputManager.enabled = false;

			//used in the update function to minimize the times it calls the function
			scriptsEnabled = false;
		}
	}

	//enables all scripts if game is paused
	public void UnPauseTurbine(bool gamePaused){
		if(gamePaused == false){
			//enable animation
			turbineAnim.enabled = true;
			turbineAnim.GetComponent<Animator>().enabled = true;

			turbineDmg.enabled = true;
			repair.enabled = true;
			turbineSpawner.enabled = true;
			inputManager.enabled = true;
			
			//used in the update function to minimize the times it calls the function
			scriptsEnabled = true;
		}
	}

	

}
