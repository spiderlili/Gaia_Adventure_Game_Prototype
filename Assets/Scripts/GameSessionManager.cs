using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] IconHP heartsUI; 

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSessionManager>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + score.ToString();

    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString(); //update the text
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();           
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        heartsUI.TakeAwayLife();
        playerLives--;
        livesText.text = livesText.text = "Lives: " + playerLives.ToString();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        var dieSceneIndex = SceneManager.sceneCountInBuildSettings;
        //load die scene - the last scene in the build settings
        SceneManager.LoadScene(dieSceneIndex - 1);
        Destroy(gameObject);
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }

}