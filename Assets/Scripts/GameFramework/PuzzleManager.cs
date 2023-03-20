using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleSuccessReceptor 
{
    public void OnPuzzleSuccess();
    public void OnPuzzleFailure();
}

public class PuzzleManager : Listener
{
    public GameObject[] PuzzleSuccessReceiver;
    bool puzzleCompletedFirstTime;
    [HideInInspector] public bool puzzleCompleted;

    public void OnPuzzleComplete()
    {
        if (!puzzleCompleted)
        {
            Debug.Log("Puzzle Completed");

            if (PuzzleSuccessReceiver != null)
            {
                foreach (GameObject puzzleReceiver in PuzzleSuccessReceiver)
                {
                    IPuzzleSuccessReceptor receptor = GetPuzzleReceptorFromGameObject(puzzleReceiver);

                    if (receptor != null)
                    {
                        receptor.OnPuzzleSuccess();

                        if (AudioManager.Instance)
                        {
                            if (!puzzleCompletedFirstTime)
                            {
                                AudioManager.Instance.PlayOnPuzzleComplete();
                                puzzleCompletedFirstTime = true;
                            }
                        }
                    }
                }
            }

            puzzleCompleted = true;
        }
    }

    void OnPuzzleFailure() 
    {
        if (puzzleCompleted)
        {
            Debug.Log("Puzzle Resetted");

            if (PuzzleSuccessReceiver != null)
            {
                foreach (GameObject puzzleReceiver in PuzzleSuccessReceiver)
                {
                    IPuzzleSuccessReceptor receptor = GetPuzzleReceptorFromGameObject(puzzleReceiver);

                    if (receptor != null)
                    {
                        receptor.OnPuzzleFailure();
                    }
                }
            }

            puzzleCompleted = false;
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

        if (ammountOfCompleted == subjects.Count)
        {
            OnPuzzleComplete();
        }
        else 
        {
            OnPuzzleFailure();
        }
    }

    public void OnDrawGizmos()
    {
        if (PuzzleSuccessReceiver != null)
        {
            foreach (GameObject puzzleReceiver in PuzzleSuccessReceiver)
            {
                if (puzzleReceiver != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, puzzleReceiver.transform.position);
                }
            }
        }
    }

    public IPuzzleSuccessReceptor GetPuzzleReceptorFromGameObject(GameObject puzzleReceiver) 
    {
        IPuzzleSuccessReceptor receptor = puzzleReceiver.GetComponent<IPuzzleSuccessReceptor>();

        if (receptor == null)
        {
            receptor = puzzleReceiver.GetComponentInChildren<IPuzzleSuccessReceptor>();
        }

        return receptor;
    }

}
