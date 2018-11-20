using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHeartHP : MonoBehaviour {
    public bool showHP; //whether the HP is full
    public GameObject heartFull; //shows the full heart if showHP is true 
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (showHP == true)
        {
            heartFull.GetComponent<Image>().enabled = true;
        }
        else {
            heartFull.GetComponent<Image>().enabled = false;
        }
	}
}
