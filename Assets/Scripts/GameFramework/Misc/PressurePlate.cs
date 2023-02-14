using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Subject
{
    public GameObject entityReactor;
    public bool canBeToggle = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotifyListener();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBeToggle) 
        {
            if (other.CompareTag("Player")) 
            {
                Reset();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (entityReactor != null) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, entityReactor.transform.position);
        }
    }


}
