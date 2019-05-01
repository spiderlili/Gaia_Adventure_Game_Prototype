using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _runSpeed = 5.0f;
    [SerializeField] float _jumpSpeed = 6.0f;
    [SerializeField] float _climbSpeed = 5.0f;
    [SerializeField] Vector2 _deathKick = new Vector2(10f, 10f); //tweak arount these values for dramatic death kick motion 
    [SerializeField] bool _onJumpPad = false;
    [SerializeField] float _jumpPadMultiplier = 1.5f;
    [SerializeField] float _dashSpeed = 7.0f; //how long the dash lasts
    [SerializeField] float _dashTime = 3.0f;
    [SerializeField] public float startDashTime;
    [SerializeField] public Weapon weapon;
    [SerializeField] public GameObject dashVFX;
    [SerializeField] public GhostDoubleImageFX ghostFX;
    private int _direction;

    //player state
    bool isAlive = true;
    public bool isLeft = false;

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
        playerFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;
        _dashTime = startDashTime;
    }

    void Shoot()
    {
        weapon.Shoot();
    }

    void Update(){ 
        if (!isAlive){
            return; //turn off the player's ability to control the character if they died
        }
        Run();
        Climb();
        Jump();
        FlipSprite();
        Die();
        Shoot();
        //TODO: Hook up dash control scheme
        //Dash(); 


    }

    //NOT IN USE
    private void Dash()
    {
        //if the player is not dashing
        if (_direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ghostFX.makeGhost = true;
                _direction = 1;
                Instantiate(dashVFX, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ghostFX.makeGhost = true;
                _direction = 2;
                Instantiate(dashVFX, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                ghostFX.makeGhost = true;
                _direction = 3;
                Instantiate(dashVFX, transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ghostFX.makeGhost = true;
                _direction = 4;
                Instantiate(dashVFX, transform.position, Quaternion.identity);
            }
        }
        else
        {
            //decrease dash time or dash will never stop
            if (_dashTime <= 0)
            {
                _direction = 0;
                _dashTime = startDashTime;
                playerRigidBody.velocity = Vector2.zero; //stop the player from continuing to dash
            }
            else
            {
                _dashTime -= Time.deltaTime;
                playerAnimator.SetTrigger("Shake"); //screen shake when dashing
                if (_direction == 1)
                {
                    playerRigidBody.velocity = Vector2.left * _dashSpeed;
                }
                if (_direction == 2)
                {
                    playerRigidBody.velocity = Vector2.right * _dashSpeed;
                }
                /*
                if (_direction == 3)
                {
                    playerRigidBody.velocity = Vector2.up * _dashSpeed;
                }
                if (_direction == 4)
                {
                    playerRigidBody.velocity = Vector2.down * _dashSpeed;
                }*/
            }
        }
    }

    private void Jump(){
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return; //prevent the player from being able to jump if they hit the wall
        }
        if (Input.GetButtonDown("Jump"))
        {          
            Vector2 jumpVelocityToAdd = new Vector2(0f, _onJumpPad ? _jumpSpeed * _jumpPadMultiplier : _jumpSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
            Debug.Log("jumpVelocityToAdd: " + jumpVelocityToAdd);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("JumpPad")){
            _onJumpPad = true;
            Debug.Log("onjumppad true");
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("JumpPad")){
            _onJumpPad = false;
            Debug.Log("onjumppad false");
        }

    }
    private void Run(){
        float controlThrow = Input.GetAxis("Horizontal"); //value between -1 and 1
        Vector2 playerVelocity = new Vector2(controlThrow * _runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
        ghostFX.makeGhost = true;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void Die(){
        if (playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            //camera go black
            isAlive = false;
            playerAnimator.SetTrigger("isDying");
            GetComponent<Rigidbody2D>().velocity = _deathKick;
            FindObjectOfType<GameSessionManager>().ProcessPlayerDeath();
        }
    }

    private void Climb(){
        if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("isClimbing", false); //stop the climb animation if exits the state
            playerRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }
        ghostFX.makeGhost = false;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * _climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        playerRigidBody.gravityScale = 0f; //zero gravity when the player climbing on a ladder

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
            isLeft = true;
        }
    }
}