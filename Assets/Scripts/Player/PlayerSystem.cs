using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public enum EPlayerState { None, Player_Idle, Player_Jump, Player_Fall, Player_Walk }

[RequireComponent(typeof(CharacterController))]
public class PlayerSystem : MonoBehaviour {
    // Player variable settings
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float jumpSpeed = 6f;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float cameraSens = 2f;

    // References
    [HideInInspector] public EPlayerState ePlayerState = EPlayerState.None;
    [HideInInspector] public CharacterController characterController = null;

    private PlayerState previousState = null;
    private PlayerState currentState = null;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetState(new PlayerState_Idle(this));
    }

    private void Update() {
        StartCoroutine(currentState.Process());

        PlayerMovement();
        PlayerRotation();
    }

    public void SetState(PlayerState state) {
        previousState = currentState;
        if (previousState != null) StartCoroutine(currentState.End());

        currentState = state;
        StartCoroutine(currentState.Start());
    }

    // Move the player
    public void PlayerMovement() {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
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

        // Move the controller
        if (characterController.enabled) characterController.Move(moveDirection * Time.deltaTime);
    }

    // Player and Camera rotation
    public void PlayerRotation() {
        rotationX += -Input.GetAxis("Mouse Y") * cameraSens;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * cameraSens, 0);
    }
}
