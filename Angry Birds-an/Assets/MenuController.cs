using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject successScreen;
    public GameObject lastLevelSuccess;
    bool isPaused;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SuccessGame()
    {
        int scene = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print(currentSceneIndex);

        if(currentSceneIndex == scene - 1)  //LastScene
        {
            lastLevelSuccess.SetActive(true);
        }
        else
        {
            successScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void NextGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}
