<<<<<<< HEAD
﻿using System.Collections;
=======
using System.Collections;
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
=======
public class LevelExit : MonoBehaviour {
	[SerializeField] float LevelLoadDelay = 2f;
  [SerializeField] float LevelExitSlowMoFactor = 0.2f;
	
  void OnTriggerEnter2D(Collider2D collision){
    StartCoroutine(LoadNextLevel());
	  
  }  
  
  IEnumerator LoadNextLevel(){
    Time.timeScale = LevelExitSlowMoFactor;
    yield return new WaitForSecondsRealtime(LevelLoadDelay);
    Time.timeScale = 1f;
    
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}
}
>>>>>>> c80d4f9f27bcec847d8a2f067e550be99d0e2597
