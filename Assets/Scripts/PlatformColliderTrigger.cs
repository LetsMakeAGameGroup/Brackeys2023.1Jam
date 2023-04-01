using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColliderTrigger : MonoBehaviour
{
    [HideInInspector] public Vector3 movement;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out PlayerMovement player)) {
            player.externalMoveSpeed = movement;
        } else {
            other.gameObject.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.TryGetComponent(out PlayerMovement player)) {
            player.externalMoveSpeed = movement;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent(out PlayerMovement player)) {
            player.externalMoveSpeed = Vector3.zero;
        } else {
            other.gameObject.transform.SetParent(null, true);
        }
    }
}
