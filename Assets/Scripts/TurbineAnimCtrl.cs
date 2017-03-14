using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineAnimCtrl : MonoBehaviour {
	Animator animator;
	public bool isRotating = true;
    private TurbineInputManager inputManager;
	private GameObject spawnManager;

    // Use this for initialization
    void Start () {
		//accessed to get the number of change the total numbers of turbines working
		spawnManager = GameObject.FindGameObjectWithTag("Spawner");
		//accessed to get when the player is clicking upon the turbine
		inputManager = GetComponentInParent<TurbineInputManager>();
		//make turbine rotating when the game starts
		animator = GetComponent<Animator>();
		animator.SetBool("Rotate",true);
	}
	
	// Update is called once per frame
	void Update () {
		if(inputManager.turbineIsClicked == true && isRotating == true){
			//disable rotation
			animator.SetBool("Rotate",false);
			isRotating = false;
			inputManager.turbineIsClicked = false;
			spawnManager.GetComponent<SpawnObjectManager>().numberOfTurbinesOperating--;
		}
		if(inputManager.turbineIsClicked == true && isRotating == false){
			//enable rotation
			animator.SetBool("Rotate",true);
			isRotating = true;
			inputManager.turbineIsClicked = false;
			spawnManager.GetComponent<SpawnObjectManager>().numberOfTurbinesOperating++;
		} 
	}

}
