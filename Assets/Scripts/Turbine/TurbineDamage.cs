using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineDamage : MonoBehaviour {
	
	private Simulation simulator;
	public bool isDamaged = false;
    private TurbineController turbine;
	private float propabilityMultiplier;
	private float turbineUsage;
    private int damageStartTime;
	private float rate;

     void Start(){			
         damageStartTime = 4;
         float startCall = Random.Range(0.0f,90.0f);
         float rate = Random.Range(120.0f,300.0f);
         simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
         turbine = GetComponent<TurbineController>();
         InvokeRepeating("CalculateDamagePropability",startCall,rate);		
     }
	
	/* 
	=====================================
		Calculate the propability that a
		turbine can get damaged
 	=====================================
	*/
	void CalculateDamagePropability(){
		if(simulator.minutesCount >= damageStartTime  &&  turbine.IsRotating() == true && turbine.IsDamaged() == false
			&& TurbineController.damagedTurbines <= 4){

			propabilityMultiplier = Random.Range(0.0f,1.0f);
			turbineUsage = 0.0f;
			if( string.Compare(simulator.powerUsage,"-Over power") == 0 ){
				turbineUsage = 1.3f;
			}
			else if(string.Compare(simulator.powerUsage,"-Correct power") == 0 ){
				turbineUsage = 1.0f;
			}
			else {
				turbineUsage = 0.0f;
			}
			float damagePropability = turbineUsage * propabilityMultiplier;
			if(damagePropability > 0.85){
				damageTurbine();
			} 
		}
	}

	/*
	damages the turbine and stops it's operation
	*/
	void damageTurbine(){
			turbine.DisableTurbine();
			isDamaged = true;
			turbine.setRepair(false);
			TurbineController.damagedTurbines++;
	}

}
