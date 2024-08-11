using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;

    public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void Setting()
    {
        settingsWindow.SetActive(true);
    }

    public void Close() 
    {
        settingsWindow.SetActive(false);
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("Credit");
    }


    public void Quit()
    {
        Application.Quit();
    }
}
