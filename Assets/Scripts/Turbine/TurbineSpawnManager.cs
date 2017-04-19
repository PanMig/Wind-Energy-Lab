using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurbineSpawnManager : MonoBehaviour {

	public GameObject turbinePrefab;
	public Transform spawnPointUp;
	public Transform spawnPointDown;
	public int numberOfTurbines = 0;
	public int numberOfTurbinesOperating = 0;
	public bool buttonPressed = false;
    public int maxNumberOfTurbines = 10; //public to be changed from the inspector

    public void SpawnTurbine(){
		
		// shows if the spawn button is pressed. Used only in the simulation script and for optimization purposes.
		if(numberOfTurbines < maxNumberOfTurbines){
			buttonPressed = true;
		}
		//TODO: it is good to not have hard coded values in conditions and follow a more generic logic, but that must change when the all the levels finished.
		if(numberOfTurbines <5){ // the first row of turbines.
			spawnPointUp.position = new Vector3(spawnPointUp.position.x + 
			40, spawnPointUp.position.y, spawnPointUp.position.z); 
			Instantiate(turbinePrefab,spawnPointUp.position,spawnPointUp.rotation); // adds turbines to the specified transform point (spawnPointUp).
			numberOfTurbines ++;
			numberOfTurbinesOperating++;
		}
		else if(numberOfTurbines < maxNumberOfTurbines && numberOfTurbines >=5 ){
			spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
			39, spawnPointDown.position.y, spawnPointDown.position.z);
			Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation); // adds turbines to the specified transform point(spawnPointDown).
			numberOfTurbines ++;
			numberOfTurbinesOperating++;	
		}
	}

}
