using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private float move_Speed = 3.5f;
    private bool goLeft; //to determine whether we are going to go left or right

    private Animator anim;
    private float jump_Force = 5f, second_Jump_Force = 7f;
    
    private bool first_Jump, second_Jump;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); //get the animator component attached to the bird itself
            
    }


    void Start () {
        
	}
	
	void OnEnable()
    {

    }


	void Update () {

        //Only if we are playing the game, the bird shall move.
        if (GameplayController.instance.playGame)
        {
            Move();

            //returns true during the frame the user pressed the given mouse button
            if (Input.GetMouseButtonDown(0)) //0 is for the left mouse button click 
            {

                JumpFunc();

            }


        }        

    }


    void Move()
    {
        if (goLeft) //ns : myBody.velocity.y keeps it unaffected.. and only the parameter where the speed needs to be changed is mentioned
        {
            myBody.velocity = new Vector2(-move_Speed,myBody.velocity.y);
        }

        else //we move right
        {
            myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
        }

    }

    void JumpFunc()
    {
        if (first_Jump) //ns : myBody.velocity.y keeps it unaffected.. and only the parameter where the speed needs to be changed is mentioned
        {

            first_Jump = false; //disabling it so that we don't have infinite jumps
            myBody.velocity = new Vector2(myBody.velocity.x,jump_Force);
            anim.SetTrigger(TagManager.FLY_TRIGGER); // set the fly trigger which moves it from the idle state to the fly state

            SoundManager.instance.PlayJumpSound();
        }

        else if (second_Jump)
        {
            second_Jump = false; //disabling it so that we don't have infinite jumps
            myBody.velocity = new Vector2(myBody.velocity.x, second_Jump_Force);
            anim.SetTrigger(TagManager.FLY_TRIGGER);

            SoundManager.instance.PlayJumpSound();

        }

    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == TagManager.BORDER_TAG) //this is done so that once you collide, you go right
        {
            goLeft = !goLeft;
            sr.flipX = goLeft; //makes the bird face the direction it is going towards
        }

        if (target.gameObject.tag == TagManager.GROUND_TAG)
        {
            /* this is done to prevent the bird from jumping to the next platform without even properly landing.. 
             * like infinite jumps without landing
              
             *This is because when we jump, we are adding force to the y velocity.. and it makes the y velocity go well above 1
             * However, when we land, the velocity  starts to decrease and go equal to or below 1*/
            if (myBody.velocity.y <= 1f) 
            {
                first_Jump = true;
                second_Jump = true;
            }
        }

        if (target.gameObject.tag == TagManager.DOG_TAG) //if we collide with the dog, we call gameover
        {
            GameplayController.instance.GameOver();
            myBody.velocity = new Vector2(0f, 0f);
            anim.Play(TagManager.DEAD_ANIMATION);

            SpawnerScript.instance.CancelWarningSpawner();
        }

    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.SCORE_TAG) //to check collisions with the empty game object (a child of the ground object)
        {
            GameplayController.instance.DisplayScore(1, 0); //using the object to access the function in the class to increase score by 1, increase diamond score by 0
            target.gameObject.SetActive(false);
        }

        if (target.tag == TagManager.DIAMONG_TAG) //we have collided with the diamond and can pick it up
        {
            GameplayController.instance.DisplayScore(0, 1); //increase score by 0, increase diamond score by 1
            target.gameObject.SetActive(false);

            SoundManager.instance.PlayDiamondSound();
        }
        
    }

}
