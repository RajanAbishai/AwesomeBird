using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameobject : MonoBehaviour {

	

	void Update () {
        DeactivateGameObj();

    }


    void DeactivateGameObj()

        /*a Function to check if we are out of bounds of the camera so that those game objects that are out of bounds*/
    {
        if (Camera.main.transform.position.y > transform.position.y + 9f) //when the camera is away from the ground in +9 in the y coordinate
        {
        
            gameObject.SetActive(false); //deactivate game objects to prevent it from spawning too many
        }
    }



}
