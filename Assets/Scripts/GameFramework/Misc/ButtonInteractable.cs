using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Subject
{
    public void OnInteract()
    {
        NotifyListener();
    }
}
