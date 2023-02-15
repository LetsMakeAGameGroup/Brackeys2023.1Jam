using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
public class PlayerInteractions : MonoBehaviour {
    public Transform handsTrans;

    [SerializeField] private float interactDistance = 2f;

    [HideInInspector] public Pickupable holdingObject;

    private void Update() {
        // Cast a ray from the camera to where the player is looking "interactDistance" away.
        Ray ray = GetComponent<PlayerCameraController>().GetPlayerCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {
            // Check if the object is interactable
            if (!hit.transform.TryGetComponent(out IInteractable interactable)) {
                UIManager.Instance.ToggleInteractionUI(this, null);
                return;
            }

            UIManager.Instance.ToggleInteractionUI(this, interactable);

            // Check if player is trying to interact
            if (Input.GetButtonDown("Interact")) {
                interactable.OnInteract(this);
            }
        } else {
            UIManager.Instance.ToggleInteractionUI(this, null);
        }

        // Check if player is trying to drop holding object
        if (Input.GetButtonDown("Fire1")) {
            if (holdingObject != null) {
                holdingObject.OnThrow(this);
                holdingObject = null;
            }
        }
    }
}
