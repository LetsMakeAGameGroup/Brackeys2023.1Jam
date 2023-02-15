using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        //If ball goes into the hoop
        if (other.CompareTag("Ball")) 
        {
            //Notifty he puzzle
            NotifyListener();
        }
    }
}
