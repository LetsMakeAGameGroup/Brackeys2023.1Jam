using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleSuccessReceptor 
{
    public void OnPuzzleSuccess();
}

public class PuzzleManager : Listener
{
    public GameObject PuzzleSuccessReceiver;

    public void OnPuzzleComplete() 
    {
        Debug.Log("Puzzle Completed");

        if (PuzzleSuccessReceiver)
        {
            IPuzzleSuccessReceptor receptor = PuzzleSuccessReceiver.GetComponent<IPuzzleSuccessReceptor>();

            if (receptor != null)
            {
                receptor.OnPuzzleSuccess();
            }
        }
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

    public void OnDrawGizmos()
    {
        if (PuzzleSuccessReceiver != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, PuzzleSuccessReceiver.transform.position);
        }
    }

}
