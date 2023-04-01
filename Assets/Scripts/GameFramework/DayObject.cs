using UnityEngine;

public class DayObject : MonoBehaviour {
    [SerializeField] private int[] daysSeen;

    public bool IsActiveOnDay(int day) {
        foreach (int daySeen in daysSeen) {
            if (daySeen == day) return true;
        }
        return false;
    }
}
