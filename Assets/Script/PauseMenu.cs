using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsWindow;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else 
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        Move_Players.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        settingsWindow.SetActive(false);
        gameIsPaused = true;
    }
    public void Resume()
    {
        Move_Players.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void LoadMainMenu() 
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
