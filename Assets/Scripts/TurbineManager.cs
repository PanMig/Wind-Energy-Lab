using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineManager : MonoBehaviour {

	public GameObject turbinePrefab;
	public Transform spawnPointUp;
	public Transform spawnPointDown;
	public int numberOfTurbines=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnTurbine(){
		if(numberOfTurbines <=8){
			spawnPointUp.position = new Vector3(spawnPointUp.position.x + 
			25, spawnPointUp.position.y, spawnPointUp.position.z);
			Instantiate(turbinePrefab,spawnPointUp.position,spawnPointUp.rotation);
			numberOfTurbines ++;
		}
		else if(numberOfTurbines <=14 && numberOfTurbines >8 ){
			spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
			25, spawnPointDown.position.y, spawnPointDown.position.z);
			Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation);
			numberOfTurbines ++;	
		}
		else {
			print("maximum number of turbines added");	
		}
		
	}
}
