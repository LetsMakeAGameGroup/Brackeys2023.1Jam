using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(DayManager), true)]
[CanEditMultipleObjects]
public class DayManagerEditor : Editor {
    SerializedProperty currentDay;

    private void OnEnable() {
        currentDay = serializedObject.FindProperty("currentDay");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(currentDay);

        if (EditorGUILayout.LinkButton("Reload DayObjects")) {
            List<GameObject> dayObjects = FindObjectsOfType<DayObject>(true).Select(dayObject => dayObject.gameObject).ToList();

            if (currentDay.intValue == 0) {
                foreach (GameObject dayObject in dayObjects) {
                    dayObject.SetActive(true);
                }
            } else {
                foreach (GameObject dayObject in dayObjects) {
                    if (dayObject.activeSelf && !dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay.intValue)) {
                        dayObject.SetActive(false);
                    } else if (!dayObject.activeSelf && dayObject.GetComponent<DayObject>().IsActiveOnDay(currentDay.intValue)) {
                        dayObject.SetActive(true);
                    }
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
