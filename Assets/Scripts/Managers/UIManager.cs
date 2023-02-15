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
}
