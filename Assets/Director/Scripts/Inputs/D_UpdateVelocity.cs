using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateVelocity : D_InputObject
{
    public float updateInterval = 0.1f;
    public int[] dataIntervals = new int[0];

    private int updatesPerSecond;
    private D_CircularArray<Vector3> positions;
    private int largest = 0;
    private float counter = 0;
    
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
        positions = new D_CircularArray<Vector3>(largest*updatesPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= updateInterval)
        {
            counter = 0;
            positions.Add(director.getPlayer().transform.position);
            foreach (var val in dataIntervals)
            {
                int refrence = (val * updatesPerSecond * -1) - 1;
                if (positions.testValid(refrence))
                {
                    float totalDistance = 0;
                    for (int i = refrence; i < 0; i++)
                    {
                        totalDistance += Vector3.Distance(positions.Get(0), positions.Get(i));
                    }

                    director.getData().getFloat("Velocity " + val).value = totalDistance / val;
                }
            }
   
        }
    }


}
