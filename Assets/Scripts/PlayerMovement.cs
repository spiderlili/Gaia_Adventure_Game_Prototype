using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(250f, 250f); //tweak arount these values for dramatic death kick motion 
    //state
    bool isAlive = true;

    //cached component references
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider2D;
    BoxCollider2D playerFeet;
    float gravityScaleAtStart;

    void Start(){
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
<<<<<<< HEAD
        playerFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;

    }

    void Update(){ 
        if (!isAlive){
            return; //turn off the player's ability to control the character if they died
        }
=======
	playerFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;

    }
	
	// Update is called once per frame
	void Update () {
	if(!isAlive){
		return; //turn off the player's ability to control the character
	}
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
        Run();
        Climb();
        Jump();
        FlipSprite();
<<<<<<< HEAD
        Die();
    }

    private void Jump(){
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return; //prevent the player from being able to jump if they hit the wall
        }
        if (Input.GetButtonDown("Jump"))
        {
=======
	Die();
	}
    private void Jump() {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Jump")) {
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Run(){
        float controlThrow = Input.GetAxis("Horizontal"); //value between -1 and 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }
	
    private void Die(){
	    if(playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))){
		    //camera go black
		    isAlive = false;
		    playerAnimator.SetTrigger("Die");
		    GetComponent<Rigidbody2D>().velocity = deathKick;
		    FindObjectOfType<GameSessionManager>().ProcessPlayerDeath();
	    }
	   
    }

<<<<<<< HEAD
    private void Die(){
        if (playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            //camera go black
            isAlive = false;
            playerAnimator.SetTrigger("isDying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSessionManager>().ProcessPlayerDeath();
        }
    }

    private void Climb(){
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("isClimbing", false); //stop the climb animation if exits the state
            playerRigidBody.gravityScale = gravityScaleAtStart;
=======
    private void Climb()
    {
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            playerAnimator.SetBool("Climbing", false); //stop the climb animation if exits the state
	    playerRigidBody.gravityScaleAtStart = gravityScaleAtStart;
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
<<<<<<< HEAD
        playerRigidBody.gravityScale = 0f; //zero gravity when the player climbing on a ladder

=======
	playerRigidBody.gravityScaleAtStart = 0f; //zero gravity when the player climbing on a ladder
		
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    private void FlipSprite(){
        //if the player is moving horizontally reverse the current scaling of x axis
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            //reverse the furrent scaling of x asis
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }
}