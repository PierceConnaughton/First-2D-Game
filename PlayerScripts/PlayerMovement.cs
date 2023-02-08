using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    //we are using a float for moving side to side as if we are using a joystick we may not going all the way left or right
    private float dirX;
    //serializedfield lets us edit values in unity
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    // Start is called before the first frame update
    private void Start()
    {
        //get the physical body of the player
        rb = GetComponent<Rigidbody2D>();

        //get the animations for the player
        anim = GetComponent<Animator>();

        //Used for changing direction of player
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");

        //vector is the x axis and y axis
        //x will be how far in either direction we want to go and y will be our current y axis
        //To get the direction correct we multiply by the number we want as if X is a negative value we go in the right direction
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);



        //if the Jump button is pressed in this case space, the physical body will go up
        if (Input.GetButtonDown("Jump"))
        {
       
            //x will be our current velocity and y will be how far we want to go
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //update the animation
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        //if player is moving right
        if (dirX > 0f)
        {
            //start the running animation
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        //if the player is moving left
        else if (dirX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        //if the player is not moving left or right
        else
        {
            //if the player is not moving left or right stop running animation
            anim.SetBool("running", false);
        }
    }
}
