using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeamReceptor 
{
    void OnBeamHit();
    void OnBeamMiss();
}

public class LightBeamTarget : Subject, IBeamReceptor
{
    public void OnBeamHit()
    {
        NotifyListener();
    }

    public void OnBeamMiss()
    {
        Debug.Log("ojsfd");
        Reset();
    }
}
