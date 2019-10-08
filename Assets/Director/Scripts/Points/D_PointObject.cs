using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PointObject : MonoBehaviour
{
    protected bool spawnable = true;
    protected bool seen = false;
    protected bool inZone = false;
    public bool ignoreSpawnable = false;
    public bool ignoreSeen = false;
    public bool ignoreZone = false;
    
    public virtual void trigger() { }

    public virtual bool isSpawnable()
    {
        return spawnable || ignoreSpawnable;
    }

    public virtual bool isSeen()
    {
        return seen || ignoreSeen;
    }

    public virtual void setSeen(bool b)
    {
        seen = b;
    }

    public virtual bool isInZone()
    {
        return inZone || ignoreZone;
    }

    public virtual void updateZone(GameObject zone)
    {
        inZone = zone.GetComponent<Collider>().bounds.Intersects(gameObject.GetComponent<Collider>().bounds);
    }

    public virtual string getDebugText()
    {
        return "Tag: "+ gameObject.tag +
            "\nSpawnable: " + spawnable +
            "\nSeen: " + seen+
            "\ninZone: " +inZone;
    }


}
