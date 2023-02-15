using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
public class PlayerInteractions : MonoBehaviour {
    public Transform handsTrans;

    [SerializeField] private float interactDistance = 2f;

    private Pickupable holdingObject;

    private void Update() {
        // Cast a ray from the camera to where the player is looking "interactDistance" away.
        Ray ray = GetComponent<PlayerCameraController>().GetPlayerCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {
            if (!hit.transform.TryGetComponent(out Interactable interactable)) {
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

        if (Input.GetButtonDown("Fire1")) {
            if (holdingObject != null) {
                holdingObject.OnDrop();
                holdingObject = null;
            }
        }
    }

    public void PickupObject(Pickupable _object) {
        if (holdingObject != null) {
            holdingObject.OnDrop();
        }
        holdingObject = _object;
    }

    public bool IsHoldingObject() {
        if (holdingObject != null) return true;
        else return false;
    }
}
