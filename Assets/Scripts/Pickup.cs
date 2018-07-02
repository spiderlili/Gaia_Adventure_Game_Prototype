using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] int pointsForPickup = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSessionManager>().AddToScore(pointsForPickup);
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        Destroy(gameObject);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}