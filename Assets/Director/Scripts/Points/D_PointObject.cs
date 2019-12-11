using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class D_PointObject : D_DirectorObject
{

    protected bool spawnable = true;
    protected bool seen = false;
    protected bool inZone = false;
    [Header("Spawn Type")] 
    public spawnTypes spawnType;
    [Header("Point Config Type")]
    public bool deleteAfterSpawn = false;
    public bool ignoreSpawnable = false;
    public bool ignoreSeen = false;
    public bool ignoreZone = false;
    
    //spawned stats
    private bool spawned = false;
    private string spawnedBy = "";
    private float spawnedAt = 0;
    public float seenAt;
    private List<string> spawnReasons = new List<string>();
    private static StringBuilder sb = new StringBuilder();
    
    
    void Start()
    {
        base.Start();
    }

    public virtual void trigger()
    {
        spawned = true;
        if (deleteAfterSpawn)
        {
            foreach (Transform child in transform)
            {
                child.transform.parent = gameObject.transform.parent;
            }
            director.getPoints().removePoint(spawnType, gameObject);
            Destroy(gameObject);
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
        if (seen != b)
            seenAt = director.getData().getFloat("Total Time").value;
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
        sb.Clear();
        sb.Append("Type: " + spawnType.ToString());
        sb.Append("\nSpawned: " + spawned);
        sb.Append("\nSpawnable: " + spawnable);
        sb.Append("\nSeen: " + seen);
        sb.Append("\nSeen At: " + seenAt);
        sb.Append("\nIn Zone: " + inZone);
        sb.Append("\nSpawned By: " + spawnedBy);
        sb.Append("\nSpawned at: " + spawnedAt);
        sb.Append("\nReasons: ");
        foreach (var reason in spawnReasons)
        {
            sb.Append("\n - " + reason);
        }
        return sb.ToString();
    }

    public virtual void setSpawnReasons(string name, float time, List<string> reasons)
    {
        spawnedBy = name;
        spawnedAt = time;
        spawnReasons = reasons;
    }

    public virtual void addToSpawnTracker(GameObject obj)
    {
        director.getTracker().registerObject(obj, spawnType);
    }
    
    


}
