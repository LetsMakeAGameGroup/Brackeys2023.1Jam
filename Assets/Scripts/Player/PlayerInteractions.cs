using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
public class PlayerInteractions : MonoBehaviour {
    [SerializeField] private float interactDistance = 2f;

    private bool canInteract = false;

    private void Update() {
        // Cast a ray from the camera to where the player is looking "interactDistance" away.
        Ray ray = GetComponent<PlayerCameraController>().GetPlayerCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {
            if (!hit.transform.TryGetComponent(out Interactable interactable)) {
                UIManager.Instance.ToggleInteractionUI(false);
                return;
            }

            UIManager.Instance.ToggleInteractionUI(true);

            // Check if player is trying to interact
            if (Input.GetButtonDown("Interact")) {
                interactable.OnInteract();
            }
        } else {
            UIManager.Instance.ToggleInteractionUI(false);
        }
    }
}
