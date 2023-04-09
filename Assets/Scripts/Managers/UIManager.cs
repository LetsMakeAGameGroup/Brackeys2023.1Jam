using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [SerializeField] private Image interactUI;
    [SerializeField] private Sprite openHandInteract;
    [SerializeField] private Sprite closedHandInteract;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private GameObject winMenu;


    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Toggles the interaction UI if it hasn't been done so already.
    public void ToggleInteractionUI(PlayerInteractions player, IInteractable interactable) {
        // Closes hand if the player is holding/pushing something already
        if (player.holdingObject != null || player.pushingObject != null) {
            interactUI.enabled = true;
            interactUI.sprite = closedHandInteract;
            return;
        }

        // Disables UI if the imitator is imitating already
        else if (interactable != null && interactable.GetType() == typeof(Imitator) && player.imitators.Contains((Imitator)interactable)) {
            interactUI.enabled = false;
            return;
        }

        if (interactable != null) {
            interactUI.enabled = true;
            interactUI.sprite = openHandInteract;
        } else {
            interactUI.enabled = false;
        }
    }

    // Updates the day text to represent the current day.
    public void UpdateDayText(int day) {
        dayText.text = "Day " + day.ToString();
    }

    public void EnableWinMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        winMenu.SetActive(true);
    }
}
