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
        
        //play SFX at the camera position
        //creates an audio instance at the 3D point of the gameobject, independent of the game object which spawned it.
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