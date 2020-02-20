using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class D_Enable : D_PointObject
{
    public GameObject enableObject;

    void Start()
    {
        base.Start();
        enableObject.SetActive(false);
    }

    public override void trigger()
    {
        enableObject.SetActive(true);
        spawnable = false;
        addToSpawnTracker(enableObject);
        base.trigger();
    }


}
