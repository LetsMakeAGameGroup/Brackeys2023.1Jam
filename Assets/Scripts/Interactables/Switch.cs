using UnityEngine;

public class Switch : MonoBehaviour, IInteractable {
    [SerializeField] private GameObject triggerObject;

    public bool isTriggered = false;

    public void OnInteract(PlayerInteractions interactee) {
        isTriggered = !isTriggered;
        if (isTriggered) {
        } else {
        }
        triggerObject.SetActive(isTriggered);
    }
}
