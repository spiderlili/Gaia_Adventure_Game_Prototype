using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, 1));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, 0));
    }

    public void OptionsMenu() {

    }

    public void QuitGame() {
        Application.Quit();
    }

    public void WinReplay() {
        FindObjectOfType<GameSessionManager>().DestroyGameSession();
        SceneManager.LoadScene(0);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, 0));
    }
}