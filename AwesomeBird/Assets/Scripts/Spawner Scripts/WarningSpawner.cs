using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSpawner : MonoBehaviour {


    private float spawn_Left = -2.25f, spawn_Right=2.25f;

    private SpriteRenderer sr;

    public GameObject dogPrefab;
    private float pushForce = 10f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }



    void Start () {

        RandomPosition();
        InvokeRepeating("SpawnObstacle",Random.Range(3,5), 5); 
        //it will call the function the first time at 3 or 4 seconds and wait 5 seconds after that

        

    }

    
    


    void RandomPosition()
    {
        Vector3 temp = transform.position; //transform.position of the warning sign because it's attached to the warning sign

        if (Random.Range(0, 2) > 0)
        {
            temp.x = spawn_Left;
            sr.flipX = true; //flips the x axis
        }
        else
        {
            temp.x = spawn_Right;

        }

        transform.position = temp;

    }


    void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(dogPrefab);
        Vector3 temp = transform.position; //position of the warning spawner

        if (transform.position.x > 0) //we are on the right side
        {
            obstacle.transform.position = new Vector3(temp.x + 5f, temp.y, 0);



            obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-pushForce, obstacle.GetComponent<Rigidbody2D>().velocity.y); //negative push force because we need to go from right to left and left to right

        }
        else //we are on the left side
        {
            obstacle.transform.position = new Vector3(temp.x - 5f, temp.y, 0);

            obstacle.GetComponent<SpriteRenderer>().flipX = true;

            obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(pushForce, obstacle.GetComponent<Rigidbody2D>().velocity.y);

        }

        if (gameObject.activeInHierarchy) //if the game object is active in the hierarchy
        {
            StartCoroutine(TurnOffOnSign());
        }

    }

    IEnumerator TurnOffOnSign()
    {
        /*Result is the sign turns off for a while after the dog has gone and comes back when it is going to respawn*/

        yield return new WaitForSeconds(2f);
        sr.enabled = false; //turns off the warning sign

        yield return new WaitForSeconds(1f);
        sr.enabled = true; //turns on the sign
    }

}
