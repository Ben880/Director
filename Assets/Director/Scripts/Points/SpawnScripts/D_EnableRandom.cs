﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnableRandom : D_PointObject
{
    public bool canEnableMultipleObjects = false;
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
        int random = Random.Range(0, enableObjects.Count - 1);
        enableObjects[random].SetActive(true);
        addToSpawnTracker(enableObjects[random]);
        enableObjects.RemoveAt(random);
        if (canEnableMultipleObjects)
        {
            enableObjects.RemoveAt(random);
            spawnable = enableObjects.Count > 0;
        }
        else
        {
            spawnable = false;
        }
        base.trigger();
    }


}