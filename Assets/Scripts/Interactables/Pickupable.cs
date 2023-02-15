using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour, IInteractable {
    public void OnInteract(PlayerInteractions interactee) {
        if (interactee.holdingObject != null) return;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.SetParent(interactee.handsTrans);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Collider>().enabled = false;
        interactee.holdingObject = this;
    }

    public void OnDrop(PlayerInteractions interactee) {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.SetParent(null);
        GetComponent<Collider>().enabled = true;
    }
}
