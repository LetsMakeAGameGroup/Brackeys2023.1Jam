using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DayManager : MonoBehaviour {
    public static DayManager Instance { get; private set; }

    [SerializeField] private GameObject staticObjectArea;
    [SerializeField] private bool areaIsStatic = false;
    [SerializeField] private float sleepTransition = 1f;
    [SerializeField] private MeshRenderer wallMuralRend;
    [SerializeField] private Material[] wallMaterial;  // Element 0 will represent day 1 and element 4 represents day 5.

    private readonly int days = 5;  // Not serialized because changing this would also need to change how DayObject script works.
    private int currentDay = 0;
    private List<GameObject> dayObjects = new();

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        dayObjects = FindObjectsOfType<DayObject>().Select(dayObject => dayObject.gameObject).ToList();
    }

    private void Start() {
        StartCoroutine(OnStartGame());
    }

    private IEnumerator OnStartGame() {
        yield return new WaitForSeconds(sleepTransition);

        StartCoroutine(OnPlayerAwake());
    }

    public IEnumerator OnPlayerSleep() {
        yield return StartCoroutine(UIManager.Instance.CloseEyesUI());

        yield return new WaitForSeconds(sleepTransition);

        UIManager.Instance.EnableDaySelect();
    }

    // Increases the day by one and wraps around when on the last day.
    public IEnumerator OnPlayerAwake(int day = -1) {
        yield return new WaitForSeconds(sleepTransition);

        int previousDay = currentDay;
        if (day == -1) currentDay = (currentDay % days) + 1;
        else currentDay = day;

        if (wallMaterial[currentDay - 1] == null) {
            Debug.LogError($"Attempting to change wallMuralRend's material to wallMaterial[{currentDay - 1}] when it doesn't exist.", transform);
        } else {
            wallMuralRend.material = wallMaterial[currentDay - 1];
        }

        foreach (GameObject dayObject in dayObjects) {
            if (areaIsStatic && staticObjectArea.GetComponent<Collider>().bounds.Contains(dayObject.transform.position)) {
                if (!dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay)) {;
                    dayObject.GetComponent<DayObject>().ChangeDayToggle(previousDay, false);
                    dayObject.GetComponent<DayObject>().ChangeDayToggle(currentDay, true);
                }
            } else {
                if (dayObject.activeSelf && !dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay)) {
                    dayObject.SetActive(false);
                } else if (!dayObject.activeSelf && dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay)) {
                    dayObject.SetActive(true);
                }
            }
        }

        UIManager.Instance.UpdateDayText(currentDay);

        yield return StartCoroutine(UIManager.Instance.OpenEyesUI());
    }
}
