using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

	
	void Start () {
        /*Moved from Start to Update because camera follow didn't get applied to the new bird
         Now, moved to FindPlayer()*/
        //player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG); 

		
	}
	
	
	void Update () {

        FollowPlayer();
    		
	}

    void FollowPlayer()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG); 

        if (player) //if we have a player, follow him
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z); //follow the player on the y axis
             
        }
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
    }

}
