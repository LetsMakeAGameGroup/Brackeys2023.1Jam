using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    // Player variable settings
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float cameraSens = 2f;

    // References
    private float rotationX = 0f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        rotationX += -Input.GetAxis("Mouse Y") * cameraSens;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * cameraSens, 0);
    }

    public Camera GetPlayerCamera() {
        return playerCamera.GetComponent<Camera>();
    }
}
