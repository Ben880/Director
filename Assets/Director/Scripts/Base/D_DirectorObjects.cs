using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class D_DirectorObjects
{
    //init var
    private static bool init = false;
    //Game objects objects
    private static GameObject directorObject;
    private static GameObject player;
    private static GameObject mainCamera;
    private static GameObject currentZone;
    private static D_Director directorScript;
    //scripts
    private static D_Flags flags;
    private static D_Data data;
    private static D_SpawnTracker tracker;
    // created classes
    private static D_Points points;
    private static D_Debug d_debug;
    public D_DirectorObjects()
    {
        if (init == false)
        {
            //find these game objects
            directorObject = GameObject.FindGameObjectWithTag("Director");
            player = GameObject.FindGameObjectWithTag("Player");
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            //find these scripts
            directorScript = directorObject.GetComponent<D_Director>();
            flags = directorObject.GetComponent<D_Flags>();
            data = directorObject.GetComponent<D_Data>();
            tracker = directorObject.GetComponent<D_SpawnTracker>();
            // created classes
            points = new D_Points();
            d_debug = new D_Debug(this);
            //initialized
            init = true;
        }
        
    }

    //==========================================================
    //=================== Getters ==============================
    //==========================================================
    public D_Director getDirectorScript()
    {
        return directorScript;
    }

    public D_Debug Debug()
    {
        return d_debug;
    }

    public D_SpawnTracker getTracker()
    {
        return tracker;
    }
    
    public D_Flags getFlags()
    {
        return flags;
    }

    public GameObject getDirectorObject()
    {
        return directorObject;
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
    //==========================================================
    //=================== Methods ==============================
    //==========================================================
    public void setCurrentZone(GameObject zone)
    {
        Debug().Log("Director received zone update");
        currentZone = zone; 
        points.zoneUpdate(zone);
        
    }
}
