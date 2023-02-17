using UnityEngine;

public class Sleepable : MonoBehaviour, IInteractable {

    public void OnInteract(PlayerInteractions interactee) {
        if (interactee.holdingObject != null) return;

        StartCoroutine(DayManager.Instance.OnPlayerSleep());
    }
}
