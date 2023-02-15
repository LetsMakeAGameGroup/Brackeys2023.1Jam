using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    public Transform lightBeamEmitter;
    public LineRenderer lineRenderer;

    IBeamReceptor lastReceptor;

    public float rayLenght = 20.0f;
    public int numberOfRaysAllowed;
    RaycastHit hit;
    public LayerMask layerMask;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void FixedUpdate()
    {
        CastBeam(lightBeamEmitter.position, lightBeamEmitter.forward);
    }

    public void CastBeam(Vector3 position, Vector3 direction) 
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, (transform.position + direction * rayLenght));

        for (int i = 0; i < numberOfRaysAllowed; i++) 
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLenght, layerMask))
            {
                //Debug.DrawLine(position, hit.point, Color.red);

                position = hit.point;
                direction = hit.normal;

                lineRenderer.positionCount = i + 3;
                lineRenderer.SetPosition(i + 1, position);
                lineRenderer.SetPosition(i + 2, (position + direction * rayLenght));

                if (hit.transform.CompareTag("BeamTarget"))
                {
                    //search for a receptor, if found call on beam hit

                    lastReceptor = hit.transform.GetComponent<IBeamReceptor>();

                    if (lastReceptor != null)
                    {
                        lastReceptor.OnBeamHit();
                    }

                    lineRenderer.positionCount = lineRenderer.positionCount - 1;

                    break;
                }
            }
            else
            {
                if (lastReceptor != null) 
                {
                    lastReceptor.OnBeamMiss();
                    lastReceptor = null;
                }

                //Debug.DrawRay(position, direction * rayLenght, Color.blue);

                //If we remove the first, remove all position counts
                if (i == 0) 
                {
                    lineRenderer.positionCount = 2;
                }

                break;
            }
        }
    }
}