using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject interactUI;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Toggles the interaction UI if it hasn't been done so already.
    public void ToggleInteractionUI(bool toggle) {
        if (interactUI.activeSelf != toggle) {
            interactUI.SetActive(toggle);
        }
    }
}
