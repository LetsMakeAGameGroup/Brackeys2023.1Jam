using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySelect : MonoBehaviour {
    public static DaySelect Instance { get; private set; }

    [SerializeField] private GameObject daySelectUI;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

        // Enable day select menu
        public void EnableDaySelect() {
        daySelectUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Select the next day through the daySelectUI.
    public void SelectDayButton(int day) {
        daySelectUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(DayManager.Instance.OnPlayerAwake(day));
    }
}
