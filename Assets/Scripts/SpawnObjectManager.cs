using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectManager : MonoBehaviour {

	public GameObject turbinePrefab;
	public Transform spawnPointUp;
	public Transform spawnPointDown;
	public int numberOfTurbinesOperating=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnTurbine(){
		if(numberOfTurbinesOperating <=8){
			spawnPointUp.position = new Vector3(spawnPointUp.position.x + 
			30, spawnPointUp.position.y, spawnPointUp.position.z);
			Instantiate(turbinePrefab,spawnPointUp.position,spawnPointUp.rotation);
			numberOfTurbinesOperating ++;
		}
		else if(numberOfTurbinesOperating <=14 && numberOfTurbinesOperating >8 ){
			spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
			30, spawnPointDown.position.y, spawnPointDown.position.z);
			Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation);
			numberOfTurbinesOperating ++;	
		}
		else {
			print("maximum number of turbines added");	
		}
		
	}
}
