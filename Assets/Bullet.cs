using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifetime = 2.0f;
    public Rigidbody2D rb;
    public int damage = 40;
    //public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        /*if(player.isLeft == true)
        {
            rb.velocity = transform.right *- speed;
        }*/
       // else
        //{
            rb.velocity = transform.right * speed;
       // }
        
        Destroy(gameObject, lifetime); //destroy object after 5s for efficiency
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Debug.Log(collision.name); //obj hit
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
