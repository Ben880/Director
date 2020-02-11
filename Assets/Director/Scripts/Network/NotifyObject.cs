using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyObject
{
    private bool notified;
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
