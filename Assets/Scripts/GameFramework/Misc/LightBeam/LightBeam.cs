using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    public Transform lightBeamEmitter;
    public LineRenderer lineRenderer;

    public float rayLenght = 20.0f;
    RaycastHit hit;
    public LayerMask layerMask;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(lightBeamEmitter.position, lightBeamEmitter.forward * rayLenght);

        if (Physics.Raycast(lightBeamEmitter.position, lightBeamEmitter.forward, out hit, rayLenght, layerMask))
        {
            lineRenderer.SetPosition(1, Vector3.forward * hit.distance);

            LightBeamBase lightBase = hit.transform.GetComponent<LightBeamBase>();

            if (lightBase)
            {
                lightBase.OnLightBeamReceive(hit.point, lightBeamEmitter.forward, hit.normal);
            }
        }
        else 
        {
            lineRenderer.SetPosition(1, Vector3.forward * rayLenght);
        }
    }
}