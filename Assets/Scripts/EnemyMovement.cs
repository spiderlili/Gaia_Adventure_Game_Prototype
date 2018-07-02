<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    //state
    bool isAlive = true;

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
    [SerializeField] float moveSpeed = 1f;
    //state
    bool isAlive = true;
    
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
    //cached component references
    Rigidbody2D enemyRigidBody;
    Animator enemyAnimator;
    Collider2D enemyCollider2D;
    float gravityScaleAtStart;

<<<<<<< HEAD
    // Use this for initialization
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            enemyRigidBody.velocity = new Vector2(-moveSpeed, 0f); //move in the negative direction
        }

    }
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
}
=======
	// Use this for initialization
	void Start () {
	enemyRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if(IsFacingRight()){
				enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);
		}else{
			enemyRigidBody.velocity = new Vector2(-moveSpeed, 0f); //move in the negative direction
		}

    }
	bool IsFacingRight(){
		return transform.localScale.x > 0;
	}
	
	private void OnTriggerExit2D(Collider2D collision){
		transform.localSclae = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
	}
}
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
