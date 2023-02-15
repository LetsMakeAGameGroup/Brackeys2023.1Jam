using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour, IInteractable {
    [SerializeField] private float throwForce = 20f;

    public void OnInteract(PlayerInteractions interactee) {
        if (interactee.holdingObject != null) return;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        transform.SetParent(interactee.handsTrans);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        GetComponent<Collider>().enabled = false;

        interactee.holdingObject = this;
    }

    public void OnThrow(PlayerInteractions interactee) {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().velocity = transform.forward * throwForce;

        transform.SetParent(null);

        GetComponent<Collider>().enabled = true;
    }
}
