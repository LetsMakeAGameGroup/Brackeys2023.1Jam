using UnityEngine;

public class Switch : Interactable {
    public bool isTriggered = false;

    public override void OnInteract(PlayerInteractions interactee) {
        isTriggered = !isTriggered;
        if (isTriggered) {
            Debug.Log($"{gameObject.name} is on!");
        } else {
            Debug.Log($"{gameObject.name} is off!");
        }
    }
}
