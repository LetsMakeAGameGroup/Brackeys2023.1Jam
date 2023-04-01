using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
public class PlayerInteractions : MonoBehaviour {
    public Transform handsTrans;

    [SerializeField] private float interactDistance = 2f;

    [HideInInspector] public Pickupable holdingObject;
    [HideInInspector] public Slidable pushingObject;
    [HideInInspector] public List<Imitator> imitators = new();

    private void Update() {
        // Cast a ray from the camera to where the player is looking "interactDistance" away.
        Ray ray = GetComponent<PlayerCameraController>().GetPlayerCamera().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)) {
            // Check if the object is interactable
            if (!hit.transform.TryGetComponent(out IInteractable interactable) || (interactable != null && interactable is TimedLever && !(interactable as TimedLever).canReset && (interactable as TimedLever).compleated)) {
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

        // Check if player is trying to throw/stop pushing object
        if (Input.GetButtonDown("Fire1")) {
            if (holdingObject != null) {
                holdingObject.OnThrow(this);
                holdingObject = null;
            }

            if (pushingObject != null) {
                pushingObject.GetComponent<AudioSource>().Stop();
                pushingObject = null;
            }

            if (imitators.Count > 0) {
                imitators.Clear();
            }
        }
    }
}
