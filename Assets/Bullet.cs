using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifetime = 2.0f;
    public Rigidbody2D rb;
    public int damage = 40;
    [SerializeField] public GameObject hitFX;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); //destroy object after 5s for efficiency
    }

    private void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                //Instantiate(hitFX, transform.position, Quaternion.identity);
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("Tilemap collision");
        }
    }

    //destroy projectile if collides with level geometry - must make boxcollider triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(hitFX, transform.position, Quaternion.identity);
            Debug.Log("Tilemap collision");
        }
    }
}
