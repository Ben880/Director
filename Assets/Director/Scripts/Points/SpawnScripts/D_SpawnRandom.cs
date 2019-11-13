using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class D_SpawnRandom : D_PointObject
{

    public bool canSpawnMultipleTimes = false;
    public GameObject[] spawnObjects = new GameObject[1];
    private GameObject spawnedObject;


    public override void trigger()
    {
        spawnedObject = Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length-1)], gameObject.transform.position, gameObject.transform.rotation);
        addToSpawnTracker(spawnedObject);
        if (!canSpawnMultipleTimes)
            spawnable = false;
        if (spawnedObject != null)
            spawnable = false;
        base.trigger();
    }
    
}