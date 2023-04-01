using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DayManager : MonoBehaviour {
    public static DayManager Instance { get; private set; }

    [SerializeField] private int currentDay = 0;  // 0 is pre-game. This will immediately increase by one on game start.

    [HideInInspector] public List<GameObject> dayObjects = new();

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        dayObjects = FindObjectsOfType<DayObject>(true).Select(dayObject => dayObject.gameObject).ToList();

        AdvanceDay();
    }

    // Increases the day by one
    public void AdvanceDay() {
        currentDay++;

        if (AudioManager.Instance) {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayRandomMusic();
        }

        foreach (GameObject dayObject in dayObjects) {
            if (dayObject.activeSelf && !dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay)) {
                dayObject.SetActive(false);
            } else if (!dayObject.activeSelf && dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay)) {
                dayObject.SetActive(true);
            }
        }
    }
}
