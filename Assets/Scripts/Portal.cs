using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private void LateUpdate() {
        Vector3 playerPos = Camera.main.transform.position;
        playerPos.y = transform.position.y;

        transform.LookAt(playerPos, Vector3.up);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            DayManager.Instance.AdvanceDay();
        }
    }
}
