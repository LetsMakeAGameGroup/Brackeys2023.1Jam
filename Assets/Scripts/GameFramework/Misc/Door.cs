using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPuzzleSuccessReceptor
{
    AudioSource m_AudioSource;
    AudioClip gateSound;

    public bool isClosed = true;

    private Coroutine openGate;

    void Awake() 
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void OnPuzzleSuccess()
    {
        ToggleDoor(false);
    }

    public void OnPuzzleFailure()
    {
        ToggleDoor(true);
    }


    void ToggleDoor(bool newState) 
    {
        isClosed = newState;

        //Run door open/close animation or disable

        if (openGate == null)
        {
            if (m_AudioSource.isPlaying) 
            {
                m_AudioSource.Stop();
            }

            if (isClosed)
            {
                m_AudioSource.Play();
                openGate = StartCoroutine(OpenCloseGate(4, -10));
            }
            else 
            {
                m_AudioSource.Play();
                openGate = StartCoroutine(OpenCloseGate(4, 10));
            }
        }
    }

    IEnumerator OpenCloseGate (float duration, float newY)
    {
        float timeElapsed = 0;
        Vector3 currentPos = transform.position;

        while (timeElapsed < duration && transform.position.y != newY) 
        {
            transform.position = Vector3.Lerp(currentPos, currentPos + Vector3.up * newY, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        openGate = null;
        yield return null;
    }

    //IEnumerator CloseGate() 
    //{
    //
    //}
}
