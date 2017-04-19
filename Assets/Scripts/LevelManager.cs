using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}

	public void ExitGame(){
		Application.Quit();
	}

	//TODO : when final number of scenes will be added, the parameter must change to something more clean and dependent free.
	public void ReplayGame(){
		SceneManager.LoadScene(1);
	}
}
