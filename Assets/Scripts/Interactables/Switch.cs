using UnityEngine;

public class Switch : MonoBehaviour, IInteractable {
    public bool isTriggered = false;

    public void OnInteract(PlayerInteractions interactee) {
        isTriggered = !isTriggered;
        if (isTriggered) {
            Debug.Log($"{gameObject.name} is on!");
        } else {
            Debug.Log($"{gameObject.name} is off!");
        }
    }
}
