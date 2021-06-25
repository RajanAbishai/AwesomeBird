using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

	
	void Start () {

        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
		
	}
	
	
	void Update () {

        FollowPlayer();
    		
	}

    void FollowPlayer()
    {
        if(player) //if we have a player, follow him
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z); //follow the player on the y axis
             
        }
    }

}
