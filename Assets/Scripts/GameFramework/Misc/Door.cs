using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPuzzleSuccessReceptor
{
    public bool isClosed = true;

    public void OnPuzzleSuccess()
    {
        ToggleDoor(false);
    }

    public void OnPuzzleFailure()
    {
        ToggleDoor(true);
    }


    void ToggleDoor(bool newState) 
    {
        isClosed = newState;

        //Run door open/close animation or disable
        gameObject.SetActive(isClosed);
    }
}
