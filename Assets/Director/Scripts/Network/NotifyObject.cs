using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyObject
{
    private bool notified;
    private bool destroy;

    public bool Destroy
    {
        get { return destroy; }
        set { destroy = value; }
    }
    public void notify()
    {
        notified = true;
    }

    public void reset()
    {
        notified = false;
    }

    public bool isTriggered()
    {
        return notified;
    }
    
}
