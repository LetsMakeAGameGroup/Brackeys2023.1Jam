using UnityEngine;

public class DayObject : MonoBehaviour {
    [SerializeField] private bool firstDay = true;
    [SerializeField] private bool secondDay = true;
    [SerializeField] private bool thirdDay = true;
    [SerializeField] private bool fourthDay = true;
    [SerializeField] private bool fifthDay = true;

    public bool IsActiveOnDay(int day) {
        switch (day) {
            case 1:
                return firstDay;
            case 2:
                return secondDay;
            case 3:
                return thirdDay;
            case 4:
                return fourthDay;
            case 5:
                return fifthDay;
            default:
                Debug.LogError("Calling IsActiveOnDay on a day that is not setup.", transform);
                return false;
        }
    }
}
