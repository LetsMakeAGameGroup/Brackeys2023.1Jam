using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPuzzleSucessReceiver
{
    public void OnPuzzleSuccess()
    {
        gameObject.SetActive(false);
    }
}
