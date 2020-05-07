using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyObject
{
    // ===========================================================================================
    // Purpose: object to be notified of command execute
    // ===========================================================================================
    private bool notified;

    public bool Destroy { get; set; }

    public void Notify() { notified = true; }

    public void Reset() { notified = false; }

    public bool IsTriggered() { return notified; }
    
}
