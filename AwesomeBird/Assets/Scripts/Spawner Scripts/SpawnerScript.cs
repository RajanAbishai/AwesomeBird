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

        GameObject newGround = Instantiate(ground_Prefab);

        newGround.transform.position = new Vector3 (0f, current_Y_Position, 0f);    
    }


}
