using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour {

	private Color startColorBody;
	private Renderer rendBody;
	[SerializeField]private GameObject bladeObject;
	private Renderer rendBlades;
    private Color startColorBlades;

    // Use this for initialization
    void Start () {
		//turbine body
		rendBody = GetComponent<Renderer>();
		startColorBody = rendBody.material.color;
		//turbine blades
		rendBlades = bladeObject.GetComponent<Renderer>();
		startColorBlades = rendBlades.material.color;
	}

	public void ChangeMatColor (bool mouseIsOver){
		if(mouseIsOver == true){
				rendBody.material.color = Color.cyan;
				rendBlades.material.color = Color.cyan;
		}
		else{
			rendBody.material.color = startColorBody;
			rendBlades.material.color = startColorBlades;
		}
	}


}
