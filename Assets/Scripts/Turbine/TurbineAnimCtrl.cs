using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineAnimCtrl : MonoBehaviour {
	Animator animator;
	public bool isRotating = true;

    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool("Rotate",true);
	}

	public void DisableRotation(){
		animator.SetBool("Rotate",false);
		isRotating = false;
	}

	public void EnableRotation(){
		animator.SetBool("Rotate",true);
		isRotating = true;
	}
	public void SetRotationSpeed(int windspeed){
		if(windspeed > 18){
			animator.SetFloat("speedMultiplier",1.0f);
		}
		else if(windspeed >16 && windspeed <18){
			animator.SetFloat("speedMultiplier",0.9f);
		}
		else if(windspeed <= 16 && windspeed > 8 ){
			animator.SetFloat("speedMultiplier",0.8f);
		}
		else if(windspeed <=8 && windspeed > 5 ){
			animator.SetFloat("speedMultiplier",0.75f);	
		}
		else{
			animator.SetFloat("speedMultiplier",0.65f);
		}
	}

}
