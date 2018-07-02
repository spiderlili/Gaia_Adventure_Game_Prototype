<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour {
  Player player;
  
	void Start () {
    player = FindObjectOfType<Player>();

    }
	
	// Update is called once per frame
	void Update () {
    Vector2 newCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
    transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
	}
}
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
