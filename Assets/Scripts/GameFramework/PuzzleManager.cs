using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Listener
{

    public void OnPuzzleComplete() 
    {
        Debug.Log("Puzzle Completed");
    }

    public override void ReceiveMessage(Subject Invoker)
    {
        int ammountOfCompleted = 0;

        for (int i = 0; i < subjects.Count; i++) 
        {
            if (subjects[i].compleated)
            {
                ammountOfCompleted++;
            }
        }

        Debug.Log(ammountOfCompleted + " vs " + subjects.Count);

        if (ammountOfCompleted == subjects.Count)
        {
            OnPuzzleComplete();
        }
    }
}
