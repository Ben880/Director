using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Spawn : D_PointObject
{

    public bool canSpawnMultipleTimes = false;
    public GameObject spawnObjects = new GameObject();


    public override void trigger()
    {
        Instantiate(spawnObjects, gameObject.transform.position, gameObject.transform.rotation);
        if (!canSpawnMultipleTimes)
            spawnable = false;
    }
    
}