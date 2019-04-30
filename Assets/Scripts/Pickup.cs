using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] int pointsForPickup = 100;
    [SerializeField] private GameSessionManager gameSessionManager;

    private void Start()
    {
        gameSessionManager = FindObjectOfType<GameSessionManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameSessionManager != null && collision.gameObject.CompareTag("Player")) {
            FindObjectOfType<GameSessionManager>().AddToScore(pointsForPickup);
            //play SFX at the camera position
            //creates an audio instance at the 3D point of the gameobject, independent of the game object which spawned it.
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            //Debug.Log("Collision Detected");
            Destroy(gameObject);
        }
    }
}