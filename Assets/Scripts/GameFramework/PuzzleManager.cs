using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleSucessReceiver 
{
    public void OnPuzzleSuccess();
}

public class PuzzleManager : Subject
{
    //PuzzleBase _Puzzle;

    public GameObject OnSucessObject;

    public void OnPuzzleComplete() 
    {
        IPuzzleSucessReceiver sucessReceiver = OnSucessObject.GetComponent<IPuzzleSucessReceiver>();

        if (sucessReceiver != null) 
        {
            sucessReceiver.OnPuzzleSuccess();
        }
    }
}
