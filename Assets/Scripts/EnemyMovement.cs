﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    //state
    bool isAlive = true;

    //cached component references
    Rigidbody2D enemyRigidBody;
    Animator enemyAnimator;
    Collider2D enemyCollider2D;
    float gravityScaleAtStart;
    [SerializeField]public ParticleSystem smokeEffect;
    [SerializeField]public int health = 100;
    [SerializeField]public GameObject deathFX;

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            EnemyKill();
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

    public void EnemyKill()
    {
        smokeEffect.Stop();
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}