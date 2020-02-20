using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateVelocity : D_InputObject
{
    public float updateInterval = 0.1f;
    public int[] dataIntervals = new int[0];
    public float[] maxVelocityPercentiles = new float[0];

    private float[] peakVelocity;
    private int updatesPerSecond;
    private D_CircularList<Vector3> positions;
    private int largest = 0;
    private float counter = 0;
    private int updatesCounter;
    private float maxVelocity = 0;
    
    //private List<Tuple<float, Vector3>> positions = new List<Tuple<float, Vector3>>();
    //positions.Add(new Tuple<float, Vector3>(director.getData().getFloat("Total Time").value, director.getPlayer().transform.position));
    //private Queue<Vector3> positions;
    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // if there is no collection interval create the default array
        if (dataIntervals.Length == 0)
            dataIntervals = new int[1] {1};
        // for each data interval create a key for the director data and find the largest interval
        foreach (var val in dataIntervals)
        {
            director.getData().getFloat("Velocity " + val).value = 0;
            if (val > largest)
                largest = val;
        }
        // with the largest interval create a new circular array;
        updatesPerSecond = Mathf.RoundToInt(1 / updateInterval);
        positions = new D_CircularList<Vector3>(largest*updatesPerSecond);
        peakVelocity = new float[dataIntervals.Length];
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= updateInterval)
        {
            counter = 0;
            positions.add(director.getPlayer().transform.position);
            for (int i = 0; i < dataIntervals.Length; i++)
            {
                int refrence = (dataIntervals[i] * updatesPerSecond * -1) ;
                float totalDistance = 0;
                Vector3 lastLocation = new Vector3();
                bool first = true;
                foreach (var location in positions.getRange(refrence, 0))
                {
                    if (!first)
                    {
                        totalDistance += Vector3.Distance(lastLocation, location);
                    }
                    else
                    {
                        first = false;
                    }
                    lastLocation = location;
                }

                float velocity = totalDistance / dataIntervals[i];
                director.getData().getFloat("Velocity " + dataIntervals[i]).value = velocity;
                if (velocity > director.getData().getFloat("Peak Velocity" + dataIntervals[i]).value)
                {
                    director.getData().getFloat("Peak Velocity" + dataIntervals[i]).value = velocity;
                }

                if (velocity > director.getData().getFloat("Max Velocity").value)
                {
                    director.getData().getFloat("Max Velocity").value = velocity;
                    foreach (var val in maxVelocityPercentiles)
                    {
                        director.getData().getFloat("Max Velocity %" + val).value = velocity*val/100;
                    }
                    
                }
            }
        }
    }


}
