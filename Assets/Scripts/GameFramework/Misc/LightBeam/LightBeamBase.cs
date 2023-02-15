using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightBeamBase : MonoBehaviour
{
    public abstract void OnLightBeamReceive(Vector3 lightBeamHitPoint, Vector3 directionOfLightBeam, Vector3 normalFace);
}
