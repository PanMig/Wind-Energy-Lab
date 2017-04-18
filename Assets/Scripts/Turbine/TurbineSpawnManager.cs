using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurbineSpawnManager : MonoBehaviour {

	public GameObject turbinePrefab;
	public Transform spawnPointUp;
	public Transform spawnPointDown;
	public Transform spawnPointExtraSea;
	public int numberOfTurbines = 0;
	public int numberOfTurbinesOperating = 0;
	public bool buttonPressed = false;


	public void SpawnTurbine(){
		// shows if the spawn button is pressed. Used only in the simulation script and for optimization purposes.
		buttonPressed = true;

		//planes level
		if (SceneManager.GetActiveScene().buildIndex == 1){
			if(numberOfTurbines <5){
				spawnPointUp.position = new Vector3(spawnPointUp.position.x + 
				40, spawnPointUp.position.y, spawnPointUp.position.z);
				Instantiate(turbinePrefab,spawnPointUp.position,spawnPointUp.rotation);
				numberOfTurbines ++;
				numberOfTurbinesOperating++;
			}
			else if(numberOfTurbines <10 && numberOfTurbines >=5 ){
				spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
				39, spawnPointDown.position.y, spawnPointDown.position.z);
				Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation);
				numberOfTurbines ++;
				numberOfTurbinesOperating++;	
			}
		}
		//sea level
		else{
			if(numberOfTurbines <=4){
				spawnPointUp.position = new Vector3(spawnPointUp.position.x + 
				32, spawnPointUp.position.y, spawnPointUp.position.z);
				Instantiate(turbinePrefab,spawnPointUp.position,spawnPointUp.rotation);
				numberOfTurbines ++;
				numberOfTurbinesOperating++;
			}
			else if(numberOfTurbines <=8 && numberOfTurbines >4 ){
				spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
				38, spawnPointDown.position.y, spawnPointDown.position.z);
				Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation);
				numberOfTurbines ++;
				numberOfTurbinesOperating++;	
			}
			else if(numberOfTurbines <=12 && numberOfTurbines >8 ){
				spawnPointExtraSea.position = new Vector3(spawnPointExtraSea.position.x + 
				38, spawnPointExtraSea.position.y, spawnPointExtraSea.position.z);
				Instantiate(turbinePrefab,spawnPointExtraSea.position,spawnPointExtraSea.rotation);
				numberOfTurbines ++;
				numberOfTurbinesOperating++;	
			}
		}
	}


}
