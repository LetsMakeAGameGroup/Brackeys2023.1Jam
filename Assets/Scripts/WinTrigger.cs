using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider Collider) {
        if (Collider.gameObject.CompareTag("Player")) {
            UIManager.Instance.EnableWinMenu();
            Destroy(this);
        }
    }
}
