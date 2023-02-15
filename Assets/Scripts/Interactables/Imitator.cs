using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Imitator : MonoBehaviour, IInteractable {
    private CharacterController characterController;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    public void OnInteract(PlayerInteractions interactee) {
        if (interactee.holdingObject != null) return;

        interactee.imitators.Add(this);
    }

    public void OnImitateMovement(Vector3 moveInput) {
        if (characterController.enabled) characterController.Move(moveInput * Time.deltaTime);
    }

    public void OnImitateRotation(Quaternion rotation) {
        transform.rotation = rotation;
    }
}
