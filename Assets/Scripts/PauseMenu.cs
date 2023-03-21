using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    public bool isPaused;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenu;

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

        AudioSource[] allAudioSource = FindObjectsOfType<AudioSource>();

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            foreach(AudioSource audioSource in allAudioSource) {
                audioSource.Pause();
            }
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

            foreach (AudioSource audioSource in allAudioSource) {
                audioSource.UnPause();
            }
        }
    }

    public void Continue()
    {
        TogglePause();
    }

    public void OpenPauseMenu()
    {
        PauseMenuUI.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void OpenOptionsMenu() 
    {
        PauseMenuUI.SetActive(false);
        OptionsMenu.SetActive(true);
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
