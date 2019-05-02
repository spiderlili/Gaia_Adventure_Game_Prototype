using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject bullet;
    public Rigidbody2D bulletRigidbody;
    public float moveSpeed = 8.0f;

    private void Start()
    {
        bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
    }

    public void ShootRight()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody2D shot = Instantiate(bulletRigidbody, firePoint.position, firePoint.rotation) as Rigidbody2D;
            shot.velocity = transform.right * moveSpeed;
        }
    }

    public void ShootLeft()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody2D shot = Instantiate(bulletRigidbody, firePoint.position, firePoint.rotation) as Rigidbody2D;
            shot.velocity = -transform.right * moveSpeed;
        }
    }
}
