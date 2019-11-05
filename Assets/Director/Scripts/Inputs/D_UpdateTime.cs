using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class D_UpdateTime : D_InputObject
{
    private int totalTimeIndex;
    private int checkpointTimeIndex;
    private int checkpointVelocityIndex;
    private int lastCheckpointTimeIndex;
    private int lastCheckpointVelocityIndex;
    private GameObject lastCheckpointObject;
    private float distanceTraversed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        totalTimeIndex = director.getData().getFloatIndex("Total Time");
        checkpointTimeIndex = director.getData().getFloatIndex("Checkpoint Time");
        checkpointVelocityIndex = director.getData().getFloatIndex("Checkpoint Velocity");
        lastCheckpointTimeIndex = director.getData().getFloatIndex("Last Checkpoint Time");
        lastCheckpointVelocityIndex = director.getData().getFloatIndex("Last Checkpoint Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        director.getData().getFloat(totalTimeIndex).value += Time.deltaTime;
        director.getData().getFloat(checkpointTimeIndex).value += Time.deltaTime;
        
        if (lastCheckpointObject == null)
        {
            distanceTraversed = Vector3.Distance(director.getPlayer().transform.position, director.getPoints().getStart().transform.position);
        }
        else
        {
            distanceTraversed = Vector3.Distance(director.getPlayer().transform.position, lastCheckpointObject.transform.position);
        }
        director.getData().getFloat(checkpointVelocityIndex).value =
            distanceTraversed / director.getData().getFloat(checkpointTimeIndex).value;
        
    }

    public void applyCheckpoint(GameObject checkpoint)
    {
        director.getData().getFloat(checkpointTimeIndex).value = 0;
        lastCheckpointObject = checkpoint;
        director.getData().getFloat(lastCheckpointTimeIndex).value = director.getData().getFloat(checkpointTimeIndex).value;
        director.getData().getFloat(lastCheckpointVelocityIndex).value = director.getData().getFloat(checkpointVelocityIndex).value;
    }
}
