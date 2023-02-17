using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    public bool isPaused;
    public GameObject PauseMenuUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        PauseMenuUI.SetActive(isPaused);

        Cursor.visible = isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Continue()
    {
        TogglePause();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
