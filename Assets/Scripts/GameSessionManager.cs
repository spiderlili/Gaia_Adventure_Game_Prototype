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
    [SerializeField] Text healthText;
    [SerializeField] IconHP heartsUI;
    [SerializeField] HealthBarPortraitStyle healthBarPortraitStyle;

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
        healthText.text = "Health: " + healthBarPortraitStyle.health.ToString();
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
        healthBarPortraitStyle.RemoveHealth(334);

        if (healthBarPortraitStyle.health <= 0) {
            playerLives--;
            heartsUI.TakeAwayLife();
            healthBarPortraitStyle.RestoreFullHealth();
        }
        healthText.text = "Health: " + healthBarPortraitStyle.health.ToString();
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