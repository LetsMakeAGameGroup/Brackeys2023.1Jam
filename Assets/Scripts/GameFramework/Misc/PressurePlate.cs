using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Observer
{
    public PuzzleManager puzzleManager;

    public override void Notify(Subject subject)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        puzzleManager.OnPuzzleComplete();
    }
}
