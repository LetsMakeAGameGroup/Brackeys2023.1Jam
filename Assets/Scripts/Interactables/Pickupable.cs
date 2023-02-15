using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickupable : Interactable {
    public override void OnInteract(PlayerInteractions interactee) {
        if (interactee.IsHoldingObject()) return;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.SetParent(interactee.handsTrans);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<BoxCollider>().enabled = false;
        interactee.PickupObject(this);
    }

    public void OnDrop() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.SetParent(null);
        GetComponent<BoxCollider>().enabled = true;
    }
}
