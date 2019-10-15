using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateDistance : D_InputObject
{
    
    private GameObject start;
    private GameObject end;

    private int totalIndex;
    private int traversedIndex;

    //private Data data;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        start = GameObject.FindGameObjectWithTag("StartPoint");
        end = GameObject.FindGameObjectWithTag("EndPoint");
        totalIndex = director.getData().getFloatIndex("Total Distance");
        traversedIndex = director.getData().getFloatIndex("Traversed Distance");
        director.getData().getFloat(totalIndex).value = Vector3.Distance(start.transform.position, end.transform.position);
        if (start == null || end == null)
        {
            enabled = false;
            director.Debug().Log("D_Update Distance disables: no fount start or end pont");
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
            director.getData().getFloat(traversedIndex).value = Vector3.Distance(start.transform.position, director.getPlayer().transform.position);
    }
}
