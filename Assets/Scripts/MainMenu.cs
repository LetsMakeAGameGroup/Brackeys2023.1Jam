using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject startMenuContainer;
    GameObject optionMenuContainer;
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptionsMenu() 
    {
        startMenuContainer.SetActive(false);
        optionMenuContainer.SetActive(true);
    }

    public void OpenStartMenu() 
    {
        startMenuContainer.SetActive(true);
        optionMenuContainer.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
