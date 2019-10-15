using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class D_SpawnRandom : D_PointObject
{

    public bool canSpawnMultipleTimes = false;
    public GameObject[] spawnObjects = new GameObject[1];


    public override void trigger()
    {
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length-1)], gameObject.transform.position, gameObject.transform.rotation);
        if (!canSpawnMultipleTimes)
            spawnable = false;
    }
    
}