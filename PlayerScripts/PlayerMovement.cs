using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //calling the different components that make up the player
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coli;

    

    //we are using a float for moving side to side as if we are using a joystick we may not going all the way left or right
    private float dirX;
    //serializedfield lets us edit values in unity
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private LayerMask jumpableGround;

   //created an enum for different states player can be in
   //have the starting state as idle
   private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;


    // Start is called before the first frame update
    private void Start()
    {
        //get the physical body of the player
        rb = GetComponent<Rigidbody2D>();

        //get the animations for the player
        anim = GetComponent<Animator>();

        //Used for changing direction of player
        sprite = GetComponent<SpriteRenderer>();

        //get the collider of the player
        coli = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");

        //vector is the x axis and y axis
        //x will be how far in either direction we want to go and y will be our current y axis
        //To get the direction correct we multiply by the number we want as if X is a negative value we go in the right direction
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);



        //if the Jump button is pressed in this case space AND we are grounded, the physical body will go up
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            //x will be our current velocity and y will be how far we want to go
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //update the animation
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;

        //if player is moving right
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        //if the player is moving left
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        //if the player is not moving left or right
        else
        {
            //if the player is not moving left or right stop running animation
            state = MovementState.idle;
        }

        //we check for jumping after running and idle animation so we dont end up running in the air
        //if our body is moving upwards we start the jumping animation
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        //else if our body is going downwards we start the falling animation
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        //set the animation to the current state
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        //creates a box of the same size slightly below the player
        //this makes sure that we can only jump from the ground and doesnt cause issues if the player collides witha wall or ceiling
        //at the very end we check if we are touching the ground layer if we are we can jump if we are not we cant
        return Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
