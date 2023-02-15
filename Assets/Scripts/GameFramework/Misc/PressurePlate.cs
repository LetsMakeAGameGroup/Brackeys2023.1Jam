using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Subject
{
    public bool canBeToggle = true;
    public bool beingPressed;

    public LayerMask layerMask;
    public float pressurePlateDetectHeight = .25f;

    private void FixedUpdate()
    {
        if (Physics.CheckBox(transform.position, new Vector3(transform.localScale.x, pressurePlateDetectHeight, transform.localScale.z), transform.rotation, layerMask))
        {
            NotifyListener();
            AnimatePlate(true);

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
