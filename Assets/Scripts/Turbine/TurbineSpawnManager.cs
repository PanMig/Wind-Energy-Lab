using UnityEngine;

public class TurbineSpawnManager : MonoBehaviour {

	[Header ("Prefab")]
	public GameObject turbinePrefab;
	[Space]
	[Header ("SpawnPoint")]
	public Transform spawnPoint;
    [Space]
    [Header("Counters")]
    public Vector2 pos;
	public int numberOfTurbines = 0;
	public int numberOfTurbinesOperating = 0;
    public int maxNumberOfTurbines = 10; //public to be changed from the inspector
	public bool buttonPressed = false;

    public void SpawnTurbine(){
		
		// shows if the spawn button is pressed. Used only in the simulation script and for optimization purposes.
		if(numberOfTurbines < maxNumberOfTurbines){
			buttonPressed = true;
		}
		//TODO: it is good to not have hard coded values in conditions and follow a more generic logic, but that must change when the all the levels finished.
		if(numberOfTurbines < 5){ // the first row of turbines.
			spawnPoint.position = new Vector3(spawnPoint.position.x + 
			pos.x, spawnPoint.position.y, spawnPoint.position.z + pos.y); 
			Instantiate(turbinePrefab,spawnPoint.position,spawnPoint.rotation); // adds turbines to the specified transform point (spawnPointUp).
			numberOfTurbines ++;
			numberOfTurbinesOperating++;
		}
		
        
	}

}




/*else if(numberOfTurbines < maxNumberOfTurbines && numberOfTurbines >=5 ){
			spawnPointDown.position = new Vector3(spawnPointDown.position.x + 
			39, spawnPointDown.position.y, spawnPointDown.position.z);
			Instantiate(turbinePrefab,spawnPointDown.position,spawnPointDown.rotation); // adds turbines to the specified transform point(spawnPointDown).
			numberOfTurbines ++;
			numberOfTurbinesOperating++;	
}*/
