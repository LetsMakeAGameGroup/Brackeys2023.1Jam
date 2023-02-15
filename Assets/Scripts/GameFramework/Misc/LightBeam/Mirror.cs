using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LightBeamBase
{
    LineRenderer lineRenderer;

    public float rayLenght = 20.0f;
    RaycastHit hit;
    LayerMask layerMask;

    public void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public override void OnLightBeamReceive(Vector3 lightBeamHitPoint, Vector3 directionOfLightBeam, Vector3 normalFace)
    {
        Vector3 reflectedDirection = Vector3.Reflect(directionOfLightBeam, normalFace);

        Debug.DrawRay(lightBeamHitPoint, reflectedDirection, Color.red);

        lineRenderer.SetPosition(0, transform.InverseTransformPoint(lightBeamHitPoint));
        lineRenderer.SetPosition(1, reflectedDirection * rayLenght);

        //if (Physics.Raycast(lightBeamHitPoint, Vector3.Reflect(directionOfLightBeam, normalFace), out hit, rayLenght, layerMask))
        //{
        //    lineRenderer.SetPosition(1, Vector3.forward * hit.distance);
        //
        //    LightBeamBase lightBase = hit.transform.GetComponent<LightBeamBase>();
        //
        //    if (lightBase)
        //    {
        //        lightBase.OnLightBeamReceive(hit.point, reflectedDirection, hit.normal);
        //    }
        //}
        //else
        //{
        //    
        //}
    }
}
