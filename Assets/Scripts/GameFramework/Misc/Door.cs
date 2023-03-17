using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPuzzleSuccessReceptor
{
    AudioSource m_AudioSource;
    AudioClip gateSound;

    public bool isClosed = true;

    Vector3 initialClosedGatePosition;
    Vector3 openGatePosition;
    public float yOpenDoorDelta;
    public float openCloseSpeed;

    void Awake() 
    {
        m_AudioSource = GetComponent<AudioSource>();

        initialClosedGatePosition = transform.position;
        openGatePosition = initialClosedGatePosition + (Vector3.up * yOpenDoorDelta);
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

        if (m_AudioSource.isPlaying) 
        {
            m_AudioSource.Stop();
        }

        if (isClosed)
        {
            m_AudioSource.Play();
            StopAllCoroutines();
            StartCoroutine(CloseGate(openCloseSpeed));
        }
        else 
        {
            m_AudioSource.Play();
            StopAllCoroutines();
            StartCoroutine(OpenGate(openCloseSpeed));
        }
    }

    IEnumerator OpenGate (float speed)
    {
        float timeElapsed = 0;

        Vector3 currentPos = transform.position;
        Vector3 openGatePos = openGatePosition;

        while (currentPos != openGatePos) 
        {
            //transform.position = Vector3.Lerp(currentPos, openGatePos, timeElapsed);
            transform.position = Vector3.MoveTowards(currentPos, openGatePos, timeElapsed);
            timeElapsed += speed * Time.deltaTime;

            yield return null;
        }

        if (currentPos == openGatePos)
        {
            transform.position = openGatePos;
        }

        yield return null;
    }

    IEnumerator CloseGate(float speed)
    {
        float timeElapsed = 0;

        Vector3 currentPos = transform.position;
        Vector3 closedGatePos = initialClosedGatePosition;

        while (currentPos != closedGatePos)
        {
            //transform.position = Vector3.Lerp(currentPos, closedGatePos, timeElapsed);
            transform.position = Vector3.MoveTowards(currentPos, closedGatePos, timeElapsed);
            timeElapsed += speed * Time.deltaTime;

            yield return null;
        }

        if (currentPos == closedGatePos)
        {
            transform.position = closedGatePos;
        }

        yield return null;
    }

    //IEnumerator CloseGate() 
    //{
    //
    //}
}
