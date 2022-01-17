using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameobject : MonoBehaviour {

    private float redDogTimer = 1.2f;
	

	void Update () {
        DeactivateGameObj();

    }


    

    //need to modify this function to make the red dog appear when it collides with the warning tag and destroys when it collides with border
   void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == TagManager.WARNING_TAG)
        {
            StartCoroutine(DeactivateDog(redDogTimer));
            

        }
    }

    


    IEnumerator DeactivateDog(float timer)
    {

        //AudioManager.instance.ZombieDieSound();
        yield return new WaitForSeconds(timer); //2f original


        Destroy(gameObject);

        //gameObject.SetActive(false);

    }



    
    void DeactivateGameObj()

        /*a Function to check if we are out of bounds of the camera so that those game objects that are out of bounds*/
    {
        if (Camera.main.transform.position.y > transform.position.y + 9f) //when the camera is away from the ground in +9 in the y coordinate
        {
            //Destroy(gameObject);
            gameObject.SetActive(false); //deactivate game objects to prevent it from spawning too many
        }
    }



}
