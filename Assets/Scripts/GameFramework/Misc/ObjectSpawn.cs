using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : Listener
{
    public override void ReceiveMessage(Subject Invoker)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
