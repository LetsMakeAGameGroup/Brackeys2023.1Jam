using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {
    public void OnSceneChangeButton(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
