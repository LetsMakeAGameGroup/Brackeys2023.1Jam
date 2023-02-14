using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Listener : MonoBehaviour
{
    protected List<Subject> subjects = new List<Subject>();

    public void AddSubject(Subject newSubject) 
    {
        subjects.Add(newSubject);
    }

    public abstract void ReceiveMessage(Subject Invoker);
}
