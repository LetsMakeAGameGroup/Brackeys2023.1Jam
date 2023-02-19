using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : Subject
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If ball goes into the hoop
        if (other.CompareTag("Ball")) 
        {
            audioSource.Play();
            //Notifty he puzzle
            NotifyListener();
        }
    }
}
