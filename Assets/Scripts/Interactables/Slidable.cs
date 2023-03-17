using UnityEngine;

public class Slidable : MonoBehaviour, IInteractable {
    AudioSource audioSource;

    [SerializeField] private float pushSpeed = 2f;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnInteract(PlayerInteractions interactee) {
        if (interactee.holdingObject != null) return;

        interactee.pushingObject = this;
    }

    // Pushes the object in the direction the player is facing.
    public void OnPush(PlayerInteractions interactee, float pushForce) {
        Vector3 dir = (transform.position - interactee.transform.position) * pushForce;

        if (dir != Vector3.zero) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }

            dir.y = 0;

            // Push in only the x or y axis.
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z)) {
                dir.z = 0;
            } else {
                dir.x = 0;
            }

            dir.Normalize();

            transform.position += pushSpeed * Time.deltaTime * dir;

            // Make sure that the player isn't pushing into the object or else the object will be pushed through the wall.
            float offset = 1;
            float maxDistance = 0;
            if (dir.z == 0 && pushForce != -1f) {
                offset = Mathf.Abs(interactee.transform.position.x - transform.position.x);
                maxDistance = (interactee.transform.localScale.x / 2) + (transform.localScale.x / 2) + 0.5f;
            } else if (dir.x == 0 && pushForce != -1f) {
                offset = Mathf.Abs(interactee.transform.position.z - transform.position.z);
                maxDistance = (interactee.transform.localScale.z / 2) + (transform.localScale.z / 2) + 0.5f;
            }

            if (interactee.GetComponent<CharacterController>().enabled && offset > maxDistance) interactee.GetComponent<CharacterController>().Move(pushSpeed * Time.deltaTime * dir);
        } else {
            audioSource.Stop();
        }
    }
}
