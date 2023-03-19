using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInteractions))]
public class PlayerMovement : MonoBehaviour {

    AudioSource m_AudioSource;
    // Player variable settings
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpSpeed = 6f;

    // References
    [HideInInspector] public CharacterController characterController = null;
    [HideInInspector] public PlayerInteractions playerInteractions = null;

    private Vector3 moveDirection = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerInteractions = GetComponent<PlayerInteractions>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        // Check if in a state for pushing an object
        if (playerInteractions.pushingObject != null) {
            playerInteractions.pushingObject.OnPush(playerInteractions, Input.GetAxis("Vertical"));
            return;
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

        if (Input.GetButton("Jump") && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        }

        moveDirection.Normalize();

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded) {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        // Move the controller and imitators if there are any
        if (characterController.enabled) characterController.Move((Input.GetButton("Sprint") ? sprintSpeed : walkSpeed) * Time.deltaTime * moveDirection);

        if ((moveDirection.x > 0 || moveDirection.z > 0) && characterController.isGrounded) {
            if (!m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }
        } else {
            if (m_AudioSource.isPlaying) {
                m_AudioSource.Stop();
            }
        }

        // If there are any active imitators, imitate the input.
        if (playerInteractions.imitators.Count > 0) {
            foreach(Imitator imitator in playerInteractions.imitators) {
                imitator.OnImitateMovement(moveDirection);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (PauseMenu.Instance) PauseMenu.Instance.TogglePause();
        }
    }
}
