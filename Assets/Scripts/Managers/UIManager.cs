using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject interactUI;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private RectTransform topEyelidUI;
    [SerializeField] private RectTransform botEyelidUI;
    [SerializeField] private GameObject daySelectUI;
    [SerializeField] private float eyeSpeed = 1f;

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
        if (interactable != null) {
            // Disables UI if the player is holding something already
            if (interactable.GetType() == typeof(Pickupable) && player.holdingObject != null) {
                if (interactUI.activeSelf) {
                    interactUI.SetActive(false);
                }
                return;
            }
            // Disables UI if the player is pushing something already
            else if (interactable.GetType() == typeof(Slidable) && player.pushingObject != null) {
                if (interactUI.activeSelf) {
                    interactUI.SetActive(false);
                }
                return;
            }
            // Disables UI if the player is pushing something already
            else if (interactable.GetType() == typeof(Imitator) && player.imitators.Contains((Imitator)interactable)) {
                if (interactUI.activeSelf) {
                    interactUI.SetActive(false);
                }
                return;
            }
        }

        if (interactUI.activeSelf != (interactable != null)) {
            interactUI.SetActive((interactable != null));
        }
    }

    // Updates the day text to represent the current day.
    public void UpdateDayText(int day) {
        dayText.text = "Day " + day.ToString();
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

    public void EnableDaySelect() {
        daySelectUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Select the next day through the daySelectUI.
    public void SelectDayButton(int day) {
        daySelectUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(DayManager.Instance.OnPlayerAwake(day));
    }
}
