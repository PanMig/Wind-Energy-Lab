using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishLandscape : MonoBehaviour {
    Vector3 pos;
    string hold_landscape = GameManager.instance.hold_landscape;

    // Use this for initialization
	void Start () {
        if (goedle_sdk.GoedleAnalytics.instance.gio_interface.strategy != null){
            hold_landscape = goedle_sdk.GoedleAnalytics.instance.gio_interface.strategy["config"][0]["scenario"].Value;
        }
        if (gameObject.name != hold_landscape)
            moveGameObject(true);
        else
            moveGameObject(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void moveGameObject(bool out_of_screen){
        pos = transform.position;
        if (out_of_screen)
            pos.x = 1000;
        else
            pos.x = 0;
        transform.position = pos;
    }
}
