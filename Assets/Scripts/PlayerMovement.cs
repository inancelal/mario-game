using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemont : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float runSpeed = 10; 
    [SerializeField] float jumpSpeed = 5;
    [SerializeField] float climbSpeed = 3;   
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    bool isAlive= true;
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();   
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive){return;}
        Run();
        FlipSprite();
        OnClimbLadder();
        Die();
    }

        void OnFire(InputValue value)
    {
        if (!isAlive){return;}
        Instantiate(bullet, gun.position, transform.rotation);
    } 

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    
    void OnClimbLadder()
    {   
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
        myAnimator.SetBool("isClimbing", false);
        myRigidbody.gravityScale = 8;
        return;
        }

        Vector2 climbingVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed );
        myRigidbody.velocity = climbingVelocity;
   
        myRigidbody.gravityScale = 0;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasHorizontalSpeed);
        
    }



    void OnJump(InputValue value)
    {
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed); 
        }
    }

    void Run()
    {   
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    
        if( myRigidbody.velocity.x > 0 || myRigidbody.velocity.x < 0)
        {
            myAnimator.SetBool("isRunning", true);
        }  
        else
        {
            myAnimator.SetBool("isRunning", false);
        } 
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void Die()
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            myRigidbody.velocity += new Vector2 (10f, 15f);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity += new Vector2 (3f, 5f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        };
    }
}

