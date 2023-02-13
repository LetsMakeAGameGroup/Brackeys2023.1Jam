using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    public List<Observer> _observer = new List<Observer>();

    public void Attatch(Observer observer) 
    {
        _observer.Add(observer);
    }

    public void Detach(Observer observer) 
    {
        _observer.Remove(observer);
    }

    public void NotifyObservers() 
    {
        foreach (Observer observer in _observer) 
        {
            observer.Notify(this);
        }
    }
}
