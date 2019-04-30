using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject bullet;
   
    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
