using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    AudioSource m_AudioSource;
    // Player variable settings
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpSpeed = 6f;

    // References
    [HideInInspector] public CharacterController characterController = null;

    private Vector3 moveDirection = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        // Check if in a state for pushing an object
        if (GetComponent<PlayerInteractions>().pushingObject != null) {
            GetComponent<PlayerInteractions>().pushingObject.OnPush(GetComponent<PlayerInteractions>(), Input.GetAxis("Vertical"));
            return;
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = (Input.GetButton("Sprint") ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (Input.GetButton("Sprint") ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        } else {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded) {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        // Move the controller and imitators if there are any
        if (characterController.enabled) characterController.Move(moveDirection * Time.deltaTime);

        if ((moveDirection.x > 0 || moveDirection.z > 0) && characterController.isGrounded)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else 
        {
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
        }

        // If there are any active imitators, imitate the input.
        if (GetComponent<PlayerInteractions>().imitators.Count > 0) {
            foreach(Imitator imitator in GetComponent<PlayerInteractions>().imitators) {
                imitator.OnImitateMovement(moveDirection);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if(PauseMenu.Instance) PauseMenu.Instance.TogglePause();
        }
    }
}
