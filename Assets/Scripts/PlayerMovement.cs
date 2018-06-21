using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    //state
    bool isAlive = true;

    //cached component references
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    Collider2D playerCollider2D;
    float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;

    }
	
	// Update is called once per frame
	void Update () {
        Run();
        Climb();
        Jump();
        FlipSprite();
	}
    private void Jump() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
        } 
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal"); //value between -1 and 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Climb()
    {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            playerAnimator.SetBool("Climbing", false);
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipSprite() {
        //if the player is moving horizontally reverse the current scaling of x axis
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            //reverse the furrent scaling of x asis
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }

    }
}
