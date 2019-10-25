using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Spawn : D_PointObject
{

    public bool canSpawnMultipleTimes = false;
    public GameObject spawnObjects = new GameObject();
    private GameObject spawnedObject;


    public override void trigger()
    {
        spawnedObject = Instantiate(spawnObjects, gameObject.transform.position, gameObject.transform.rotation);
        addToSpawnTracker(spawnedObject);
        if (!canSpawnMultipleTimes)
            spawnable = false;
        if (spawnedObject != null)
            spawnable = false;
    }
    
}