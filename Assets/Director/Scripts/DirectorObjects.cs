using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DirectorObjects
{
    private static bool init = false;
    private static GameObject director;
    private static GameObject player;
    private static Flags flags;
    private static Data data;
    private static Points points;
    private static GameObject mainCamera;
    private static GameObject currentZone;
    public DirectorObjects()
    {
        if (init == false)
        {
            director = GameObject.FindGameObjectWithTag("Director");
            player = GameObject.FindGameObjectWithTag("Player");
            flags = director.GetComponent<Flags>();
            data = director.GetComponent<Data>();
            points = new Points();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            //initialized
            init = true;
        }
        
    }
    

    public Flags getFlags()
    {
        return flags;
    }

    public GameObject getDirector()
    {
        return director;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public Data getData()
    {
        return data;
    }

    public Points getPoints()
    {
        return points;
    }

    public GameObject getMainCamera()
    {
        return mainCamera;
    }

    public bool isDebug()
    {
        return getFlags().getValue("Debug");
    }

    public GameObject getCurrentZone()
    {
        return currentZone;
    }

    public void setCurrentZone(GameObject zone)
    {
        Debug.Log("Director received zone update");
        currentZone = zone; 
        points.zoneUpdate(zone);
        
    }
}
