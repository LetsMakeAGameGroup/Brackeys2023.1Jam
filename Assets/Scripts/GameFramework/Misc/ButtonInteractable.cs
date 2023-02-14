using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public void OnInteract();
}

public class ButtonInteractable : Subject, IInteractable
{
    public void OnInteract()
    {
        NotifyListener();
    }
}
