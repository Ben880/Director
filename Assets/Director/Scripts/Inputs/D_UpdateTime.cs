using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class D_UpdateTime : D_InputObject
{
    private int timeIndex;
    private int checkpointIndex;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        timeIndex = m_DDirector.getData().getFloatIndex("Total Time");
        checkpointIndex = m_DDirector.getData().getFloatIndex("Checkpoint Time");
    }

    // Update is called once per frame
    void Update()
    {
        m_DDirector.getData().getFloat(timeIndex).value += Time.deltaTime;
        m_DDirector.getData().getFloat(checkpointIndex).value += Time.deltaTime;
    }

    public void applyCheckpoint()
    {
        m_DDirector.getData().getFloat(checkpointIndex).value = 0;
    }
}
