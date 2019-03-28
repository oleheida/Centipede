using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour {


    int delayInSeconds = 3;

	public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main Scene");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadEndScene()
    {
        StartCoroutine(EndGameDelay());

    }

    IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("End Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
