using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class UpdateTime : InputObject
{
    private int timeIndex;
    private int checkpointIndex;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        timeIndex = director.getData().getFloatIndex("Total Time");
        checkpointIndex = director.getData().getFloatIndex("Checkpoint Time");
    }

    // Update is called once per frame
    void Update()
    {
        director.getData().getFloat(timeIndex).value += Time.deltaTime;
        director.getData().getFloat(checkpointIndex).value += Time.deltaTime;
    }

    public void applyCheckpoint()
    {
        director.getData().getFloat(checkpointIndex).value = 0;
    }
}
