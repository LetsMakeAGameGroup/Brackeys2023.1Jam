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

            // Check if box is being pushed into a wall
            if (pushForce != -1f) {
                RaycastHit[] hitDetects = Physics.BoxCastAll(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents - new Vector3(0.01f, 0.01f, 0.01f), dir, transform.rotation, 0.02f, ~LayerMask.GetMask("IgnoreSlidable"));
                Debug.Log($"box length: {hitDetects.Length}");
                if (hitDetects.Length > 1) {
                    return;
                }
            }

            // Check if player is pulling into a wall
            if (pushForce != 1f) {
                RaycastHit[] hitDetects = Physics.BoxCastAll(interactee.GetComponent<Collider>().bounds.center, interactee.GetComponent<Collider>().bounds.extents - new Vector3(0.01f, 0.01f, 0.01f), -dir, transform.rotation, 0.02f, ~LayerMask.GetMask("IgnoreSlidable"));
                Debug.Log($"player length: {hitDetects.Length}");
                if (hitDetects.Length > 2) {
                    return;
                }
            }

            transform.position += pushSpeed * Time.deltaTime * dir;
            interactee.transform.position += pushSpeed * Time.deltaTime * dir;
        } else {
            audioSource.Stop();
        }
    }
}
