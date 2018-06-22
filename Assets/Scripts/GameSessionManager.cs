using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour {
  [SerializeField] int playerLives = 3;
  
  private void Awake(){
    int numGameSessions = FindObjectsOfType<GameSessionManager>().Length;
    if(numGameSessions > 1){
      Destroy(gameObject);
    }else{
      DontDestroyOnLoad(gameObject);
    }
  }
  
	void Start () {

    }
  
  public void ProcessPlayerDeath(){
    if(playerLives > 1){
      TakeLife();
    }else{
      ResetGameSession();
    }
  }
  
  private void ResetGameSession(){
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
  
  private void TakeLife(){
    playerLives--;
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
	
	// Update is called once per frame
	void Update () {
    Vector2 newCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
    transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
	}
}
