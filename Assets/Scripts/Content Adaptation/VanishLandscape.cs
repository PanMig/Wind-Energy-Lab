using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishLandscape : MonoBehaviour {
    Vector3 pos;

    // Use this for initialization
	void Start () {
        if (goedle_sdk.GoedleAnalytics.instance.gio_interface.strategy != null){
            GameManager.instance.hold_landscape = goedle_sdk.GoedleAnalytics.instance.gio_interface.strategy["config"][0]["scenario"].Value;
            if (gameObject.name != GameManager.instance.hold_landscape)
                moveGameObject(true);
            else
                moveGameObject(false);
        }
	}

    void moveGameObject(bool out_of_screen){
        pos = transform.position;
        if (out_of_screen)
        {
            pos.x = 1000;
        }
        else
        {
            pos.x = 0;
        }
        transform.position = pos;
    }
}
