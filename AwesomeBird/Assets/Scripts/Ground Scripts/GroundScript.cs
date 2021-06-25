using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

	
	
	
	void Update () {
        DeactivateGround();
	}

    void DeactivateGround()
    {
        
        if(Camera.main.transform.position.y>transform.position.y + 9f) //when the camera is away from the ground in +9 in the y coordinate, deactivate old ground
        {
            SpawnerScript.instance.SpawnMoreGrounds();

            gameObject.SetActive(false); //deactivate to prevent it from spawning too many
        }
    }
}
