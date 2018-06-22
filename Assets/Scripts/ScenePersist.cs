using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager;

public class Pickup : MonoBehaviour {
	[SerializeField] AudioClip pickupSFX;
	[SerializeField] int pointsForPickup = 100;
  int startingIndex;
	
  private void Awake(){
  int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
  if(numScenePersist > 1){
    Destroy(gameObject);
  }else{
    DontDestroyOnLoad(gameObject);
  }
  }  
	void Start () {
    startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    if(currentSceneIndex != startingSceneIndex){
      Destroy(gameObject);
    }
	}
}
