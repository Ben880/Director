using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class D_DirectorObjects
{
    private static bool init = false;
    private static GameObject director;
    private static GameObject player;
    private static D_Flags _dFlags;
    private static D_Data data;
    private static D_Points _dPoints;
    private static GameObject mainCamera;
    private static GameObject currentZone;
    private static D_Debug d_debug;
    public D_DirectorObjects()
    {
        if (init == false)
        {
            director = GameObject.FindGameObjectWithTag("Director");
            player = GameObject.FindGameObjectWithTag("Player");
            _dFlags = director.GetComponent<D_Flags>();
            data = director.GetComponent<D_Data>();
            _dPoints = new D_Points();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            d_debug = new D_Debug(this);
            //initialized
            init = true;
        }
        
    }

    public D_Debug Debug()
    {
        return d_debug;
    }
    

    public D_Flags getFlags()
    {
        return _dFlags;
    }

    public GameObject getDirector()
    {
        return director;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public D_Data getData()
    {
        return data;
    }

    public D_Points getPoints()
    {
        return _dPoints;
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
        Debug().Log("Director received zone update");
        currentZone = zone; 
        _dPoints.zoneUpdate(zone);
        
    }
}
