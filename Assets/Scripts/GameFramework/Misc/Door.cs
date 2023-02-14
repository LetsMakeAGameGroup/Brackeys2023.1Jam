using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPuzzleSuccessReceptor
{
    public bool isClosed = true;

    public void OnPuzzleSuccess()
    {
        ToggleDoor();
    }

    void ToggleDoor() 
    {
        isClosed = !isClosed;

        //Run door open/close animation or disable
        gameObject.SetActive(isClosed);
    }
}
