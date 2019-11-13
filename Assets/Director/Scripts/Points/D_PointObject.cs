using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class D_PointObject : D_DirectorObject
{

    protected bool spawnable = true;
    protected bool seen = false;
    protected bool inZone = false;
    [Header("Point Object")] 
    public spawnTypes spawnType;
    public bool deleteAfterSpawn = false;
    public bool ignoreSpawnable = false;
    public bool ignoreSeen = false;
    public bool ignoreZone = false;
    public GameObject triggerOther;
    private D_PointObject triggerOtherScript;
    private bool otherTriggered = false;


    void Start()
    {
        base.Start();
        triggerOtherScript = triggerOther.GetComponent<D_PointObject>();
    }

    public virtual void trigger()
    {
        if (triggerOtherScript != null && !otherTriggered)
        {
            otherTriggered = true;
            triggerOtherScript.trigger();
        }
            
    }

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

    public virtual void addToSpawnTracker(GameObject obj)
    {
        director.getTracker().registerObject(obj, spawnType);
    }


}
