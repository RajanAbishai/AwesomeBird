using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {


    public static SpawnerScript instance; //instance is the object of the SpawnerScript class
    public GameObject ground_Prefab;
    private float ground_Y_Distance = 3.3f;
    private float current_Y_Position = 0f;

    /*This is because when we are spawning a new ground, the new ground is spawned at a position of current_Y_Position + ground_Y_Distance
    and the new position is then set as the new current_Y_Position and it repeats the process to spawn new grounds*/


    public GameObject[] dogs;
    private float xPos = 2.55f;



     void Awake()
    {
        MakeInstance();
    }


    void Start () {
        spawnInitialGrounds();
	}
	
	
	

    void MakeInstance()
    {
        if (instance == null) //if there is no instance, this is the instance. This refers to the class
        {
            instance = this;
        }
    }

    void spawnInitialGrounds()
    {
        for(int i = 0; i < 5; i++)
        {
            SpawnMoreGrounds();
        }
    }

    public void SpawnMoreGrounds()
    {
        current_Y_Position += ground_Y_Distance;

        GameObject newGround = Instantiate(ground_Prefab); //spawns a new ground

        newGround.transform.position = new Vector3 (0f, current_Y_Position, 0f);

        /*After spawning a new ground, we are going to throw a random range and based on the random number, we either spawn an obstacle or not*/

        int randomForDogs = Random.Range(0, 10); // does NOT include 10. 

        if (randomForDogs > 1) // >4 would mean 50% probability
        {
            GameObject obstacle = Instantiate(dogs[Random.Range(0, dogs.Length) ] ); //returns a number that does not include the length. Randomly selects one of the dogs to spawn

            if (obstacle.tag==TagManager.WARNING_TAG) {

                obstacle.transform.position = new Vector2 (0f, newGround.transform.position.y + 0.8f); //warning sign


            }
            else
            {

                obstacle.transform.position = new Vector2(Random.Range(-xPos, xPos), newGround.transform.position.y + 0.5f); //dog

            }



        }

        


    }


}
