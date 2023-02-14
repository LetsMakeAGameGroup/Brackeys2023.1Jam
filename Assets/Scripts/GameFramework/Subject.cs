using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    public Listener listener;
    public bool compleated;

    private void Start()
    {
        if (listener) 
        {
            listener.AddSubject(this);
        }
    }

    public void NotifyListener() 
    {
        compleated = true;

        if (listener != null) 
        {
            listener.ReceiveMessage(this);
        }
    }

    public void Reset()
    {
        compleated = false;
    }

    public virtual void OnDrawGizmos()
    {
        if (listener != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, listener.transform.position);
        }
    }

}
