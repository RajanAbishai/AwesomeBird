using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {


    public static SpawnerScript instance; //instance is the object of the SpawnerScript class
    public GameObject ground_Prefab;
    private float ground_Y_Distance = 3.3f;
    private float current_Y_Position = 0f;

    public GameObject[] collectables;

    /*This is because when we are spawning a new ground, the new ground is spawned at a position of current_Y_Position + ground_Y_Distance
    and the new position is then set as the new current_Y_Position and it repeats the process to spawn new grounds*/


    public GameObject[] dogs;
    public float xPos = 2.55f;



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


        SpawnCollectables(); //and based on the random range, it will decide to spawn or not and then, it will either spawn a blue or a yellow collectable

    }

    void SpawnCollectables()
    {

        if (Random.Range(0, 2) == 1) //returns either 0 or 1
        {
            GameObject collectableItem = Instantiate(collectables[Random.Range(0,collectables.Length)]); //Length is 2 but it does not include element[2]. It includes element[0] and element[1]

            collectableItem.transform.position = new Vector2(Random.Range(-xPos,xPos), current_Y_Position+0.5f  ); //curent y position is used to position the grounds
        }

            

    }

    public void CancelWarningSpawner()
    {
        GameObject[] warnings = GameObject.FindGameObjectsWithTag(TagManager.WARNING_TAG); // all of them would be found
        for(int i=0; i < warnings.Length; i++)
        {
            warnings[i].GetComponent<WarningSpawner>().CancelInvoke(); 
            /*All the game objects that have the warning tag,have their invokes cancelled. We could say warning spawner instead of 
             * monobehaviour because the warning spawner inherits from monobehaviour. 
             * It cancels all invoke calls on this monobehaviour */
        }
    }


}
