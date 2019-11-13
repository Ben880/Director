using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnableGroup : D_PointObject
{
    public List<GameObject> enableObjects = new List<GameObject>();

    void Start()
    {
        base.Start();
        foreach (GameObject obj in enableObjects)
        {
            obj.SetActive(false);
        }
    }

    public override void trigger()
    {
        foreach (var obj in enableObjects)
        {
            obj.SetActive(true);
            addToSpawnTracker(obj);
        }
        spawnable = false;
        base.trigger();
    }


}