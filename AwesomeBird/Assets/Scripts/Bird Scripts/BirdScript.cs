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

        Move();

        //returns true during the frame the user pressed the given mouse button
        if (Input.GetMouseButtonDown(0)) //0 is for the left mouse button click 
        {
            
            JumpFunc();

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
        }

        else if (second_Jump)
        {
            second_Jump = false; //disabling it so that we don't have infinite jumps
            myBody.velocity = new Vector2(myBody.velocity.x, second_Jump_Force);
            anim.SetTrigger(TagManager.FLY_TRIGGER);

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
            first_Jump = true;
            second_Jump = true;
        }

    }


}
