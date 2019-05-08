using System.Collections;
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
    [SerializeField]private Material matRedHit;
    public GameObject damageIndicator;
    private Animator damageIndicatorAnimator;
    private Material matDefault;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageIndicatorAnimator = damageIndicator.GetComponent<Animator>();
        matDefault = spriteRenderer.material;
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
        if (health <= 0)
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
        //TODO Add deathFX
        smokeEffect.Stop();
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectiles"))
        {
            //Make the enemy flash red
            Destroy(collision.gameObject);
            Bullet bullet = collision.GetComponent<Bullet>();
            TakeDamage(bullet.damage);

            //TODO: Instantiate damage number only and destroy damage number, leave slider there           
            GameObject goDamageIndicator = Instantiate(damageIndicator, collision.gameObject.transform.position, new Quaternion());
            damageIndicatorAnimator.SetBool("IsDamaged", true);
            
            //Fixed point Numeric Format String
            goDamageIndicator.GetComponent<UIDamageIndicator>().label.text = bullet.damage.ToString("F0");     
            spriteRenderer.material = matRedHit;
            
            if (health <= 0)
            {
                EnemyKill();
            }
            else
            {
                Invoke("ResetMaterial", 1.0f);
            }
        }
    }
}