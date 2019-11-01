using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateLastSpawned : D_InputObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var values = spawnTypes.GetValues(typeof(spawnTypes));
        foreach (var val in values)
        {
            director.getData().getFloat(val.ToString() + " Spawned Last").value += Time.deltaTime;
        }
    }
}
