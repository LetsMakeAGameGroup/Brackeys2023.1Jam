using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isClosed = true;

    void ToggleDoor() 
    {
        isClosed = !isClosed;
        gameObject.SetActive(isClosed);
    }
}
