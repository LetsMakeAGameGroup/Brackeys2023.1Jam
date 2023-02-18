using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Subject
{
    AudioSource audioSource;
    public bool canBeToggle = true;
    public bool beingPressed;

    public LayerMask layerMask;
    public float pressurePlateDetectHeight = .25f;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Physics.CheckBox(transform.position, new Vector3(transform.localScale.x, pressurePlateDetectHeight, transform.localScale.z), transform.rotation, layerMask))
        {
            NotifyListener();
            AnimatePlate(true);
            audioSource.Play();

            if (!beingPressed) 
            {
                beingPressed = true;
            }
        }
        else 
        {
            if (canBeToggle)
            {
                Reset();

                if (beingPressed)
                {
                    AnimatePlate(false);
                    beingPressed = false;
                    audioSource.Play();
                }
            }
        }
    }

    void AnimatePlate(bool isPressed)
    {
        if (isPressed)
        {
            if (beingPressed) { return; }

            transform.Translate(Vector3.down * 0.05f);
            beingPressed = true;
        }
        else 
        {
            if (!beingPressed) { return; }

            transform.Translate(Vector3.up * 0.05f);
            beingPressed = false;
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x * 2, pressurePlateDetectHeight, transform.localScale.z * 2));
    }
}
