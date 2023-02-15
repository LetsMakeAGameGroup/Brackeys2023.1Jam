using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public abstract void OnInteract(PlayerInteractions interactee);
}
