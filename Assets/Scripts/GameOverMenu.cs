using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
