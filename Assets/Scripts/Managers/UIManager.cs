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
    [SerializeField] private RectTransform topEyelidUI;
    [SerializeField] private RectTransform botEyelidUI;
    [SerializeField] private float eyeSpeed = 1f;
    [SerializeField] private GameObject winMenu;

    private Vector2 topEyelidOrigin;
    private Vector2 botEyelidOrigin;
    private Vector2 centerTopEyelid;
    private Vector2 centerBotEyelid;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        // Set UI origin for when we open eyes.
        topEyelidOrigin = topEyelidUI.anchoredPosition;
        botEyelidOrigin = botEyelidUI.anchoredPosition;

        // Get position for closing eyes and set it immediately to close.
        centerTopEyelid = topEyelidUI.anchoredPosition;
        centerTopEyelid.y = 0;
        topEyelidUI.anchoredPosition = centerTopEyelid;
        centerBotEyelid = botEyelidUI.anchoredPosition;
        centerBotEyelid.y = 0;
        botEyelidUI.anchoredPosition = centerBotEyelid;
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

    // Closes the player's eyes
    public IEnumerator CloseEyesUI() {
        while (topEyelidUI.anchoredPosition != centerTopEyelid) {
            topEyelidUI.anchoredPosition = Vector2.MoveTowards(topEyelidUI.anchoredPosition, centerTopEyelid, eyeSpeed * Time.deltaTime);
            botEyelidUI.anchoredPosition = Vector2.MoveTowards(botEyelidUI.anchoredPosition, centerBotEyelid, eyeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Opens the player's eyes
    public IEnumerator OpenEyesUI() {
        while (topEyelidUI.anchoredPosition != topEyelidOrigin) {
            topEyelidUI.anchoredPosition = Vector2.MoveTowards(topEyelidUI.anchoredPosition, topEyelidOrigin, eyeSpeed * Time.deltaTime);
            botEyelidUI.anchoredPosition = Vector2.MoveTowards(botEyelidUI.anchoredPosition, botEyelidOrigin, eyeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void EnableWinMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        winMenu.SetActive(true);
    }
}
