using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameobject : MonoBehaviour {

	

	void Update () {
        DeactivateGameObj();
        getRidOfDogWalkingInTheAir();
        

    }


    void getRidOfDogWalkingInTheAir()
    {
        //float spawnLocationOfDog = GameObject.FindGameObjectWithTag(TagManager.WARNING_TAG).GetComponent<SpawnerScript>().xPos;
        //GameObject.Find("nameOfObjectYourScriptIsOn").GetComponent<move>().speed (syntax)
        //2.55f was obtained from SpawnerScript

        if (gameObject.tag==TagManager.DOG_TAG && (gameObject.transform.position.x>2.55f || gameObject.transform.position.x<-2.55f)  ) {

            //coroutine to deactivate the dog begins around this time.
            //timed in such a way that the  red dog gets deactivated around the time it touches the other border

            StartCoroutine(DeactivateDog(0.8f));


            

            
            
        }
        
        //Disable this function after making a copy because the floor and the borders are going to be made bigger
        
     //   GameObject[] dogs = GameObject.FindGameObjectsWithTag(TagManager.DOG_TAG);
        //-2.4 on the left, 2.4 on the right

       // Vector3 temp = dogs.transform.position;

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
        
            gameObject.SetActive(false); //deactivate game objects to prevent it from spawning too many
        }

        
    }



}
