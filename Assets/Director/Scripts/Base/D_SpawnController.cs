using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class D_SpawnController : D_DirectorObject
{
    
    [Header("Spawn Type")] 
    public spawnTypes spawnType; 
    //==============================================================
    //================= Spawn Quantity Rules =======================
    //==============================================================
    public enum quantityType
    {
        single, 
        batch,
        stream
    };
    [Header("Spawn Quantity Rules")]
    public quantityType spawnQuantity;
    public float baseBatchQuantity = 5;
    //==============================================================
    //================= Spawn Location Rules =======================
    //==============================================================
    // determins what points should be considered 
    public enum locationType
    {
        range,
        random,
        zones
    };
    [Header("Spawn Location Rules")] 
    public bool ignoreSeen = false;
    public locationType spawnLocation;
    public float rangeMin = 5;
    public float rangeMax = 100;
    //==============================================================
    //================= Spawn Selection Rules ======================
    //==============================================================
    // determines which point to select from all points that were considered
    public enum selectionType
    {
        first,
        random,
        closestToPlayer,
        closestToPlayerAndEnd
    };
    [Header("Spawn Selection Rules")] 
    public selectionType spawnSelection;
    //==============================================================
    //=================== Spawn Condition Rules ====================
    //==============================================================
    // logic for determining what type of health should be spawned (if point supports it)

    [Header("Spawn Condition Rules")]
    public D_Conditon[] conditions = new D_Conditon[1];
    
    
    
    public void executeLogic()
    {
        if (shouldSpawnHelath())                                            // if shouldSpawnHelath()
        {
            GameObject spawnObject = locateSpawn();                       // spawn result of locateHealth()
            if (spawnObject != null)
                spawnObject.GetComponent<D_PointObject>().trigger();
            director.getData().getFloat(spawnType.ToString() + " Spawned Last").value = 0;
        }
    }
    //==============================================================
    //================= Should Spawn Health ========================
    //==============================================================
    // method determines if the director should spawn health
    // current method is determined by health and timers
    // other methods may be determined by external factors/flags based on intensity, accuarcy and others
    // this will be done by checking for flags/values set by other classes
    private bool shouldSpawnHelath()
    {
        bool evaluation = true;
        for (int i = 0; i < conditions.Length; i++)
        {
            evaluation = evaluation && conditions[i].evaluate();
        }

        return evaluation;
    }
    //==============================================================
    //================= Frequency Check  ===========================
    //==============================================================
    //checks to see if it is time to spawn health.
    private bool frequencyCheck()
    {
        //should not reach this point if so then spawn anyway
        return true;
    }
    //==============================================================
    //==================== Locate Health ===========================
    //==============================================================
    // determines which health to spawn from a list of health points from locateHealthSpawns()
    public GameObject locateSpawn()
    {
        director.Debug().Log("locating spawn: "  + spawnType.ToString());
        List<GameObject> points = locateSpawns();
        if (points.Count == 0)
        {
            Debug.Log("Could not find any points to spawn: "  + spawnType.ToString());
            return null;
        }
        if (spawnSelection == selectionType.first)
        {
            return points[0];//need null check
        }
        if (spawnSelection == selectionType.random)
        {
            return points[Random.Range(0, points.Count)];
        }
        if (spawnSelection == selectionType.closestToPlayer)
        {
            GameObject selection = null;
            float selectedDistance = -1;
            foreach (GameObject point in points)
            {
                float currentDistance = Vector3.Distance(point.transform.position, director.getPlayer().transform.position);
                if ( currentDistance < selectedDistance || selectedDistance == -1)
                {
                    selection = point;
                    selectedDistance = currentDistance;
                }
            }

            return selection;
        }
        if (spawnSelection == selectionType.closestToPlayerAndEnd)
        {
            for (int i = points.Count-1; i >=0; i--)
            //foreach (GameObject point in points)
            {
                float playerEndDistance = Vector3.Distance(director.getPlayer().transform.position,
                    director.getPoints().getEnd().transform.position);
                if (Vector3.Distance(points[i].transform.position, director.getPoints().getEnd().transform.position) >
                    playerEndDistance)
                    points.Remove(points[i]);
            }
            GameObject selection = null;
            float selectedDistance = -1;
            foreach (GameObject point in points)
            {
                float currentDistance = Vector3.Distance(point.transform.position, director.getPlayer().transform.position);
                if ( currentDistance < selectedDistance || selectedDistance == -1)
                {
                    selection = point;
                    selectedDistance = currentDistance;
                }
            }

            return selection;
        }
        
        return null;
    }
    //==============================================================
    //================= Locate Health Spawns =======================
    //==============================================================
    // locates all points and then checks if each one is spawnable using isPointSpawnable()
    // then returns a list of points that are spawnable
    private List<GameObject> locateSpawns()
    {
        List<GameObject> spawnablePoints = new List<GameObject>();
        foreach (GameObject point in director.getPoints().getPointsOfType(spawnType))
        {
            if (isPointSpawnable(point))
            {
                spawnablePoints.Add(point);
            }
        }
        director.Debug().Log(gameObject.name + " Found spawnable " +  spawnType.ToString()+ " points found: " +
                             spawnablePoints.Count + "\nOut of: " +
                             director.getPoints().getPointsOfType(spawnType).Count);
        return spawnablePoints;
    }
    //==============================================================
    //================= Is Point Spawnable =========================
    //==============================================================
    // returns if a point is spawnable
    private bool isPointSpawnable(GameObject point)
    {
        D_PointObject pointScript = point.GetComponent<D_PointObject>();
        if (!pointScript.isSpawnable())                 //if point says its not spawnable return false
            return false;
        if (spawnLocation == locationType.random)    //if location type = random  return true
            return isUnseen(pointScript);
        if (spawnLocation == locationType.zones)     //if location type = zones return inZone || ignoreZones
            return (pointScript.isInZone() 
                   || pointScript.ignoreZone) 
                    && isUnseen(pointScript);
        if (spawnLocation == locationType.range)     //if location type = range get distance & return evaluation
        {
            float distance = Vector3.Distance(director.getPlayer().transform.position, point.transform.position);
            return (distance >= rangeMin && distance <= rangeMax) && isUnseen(pointScript);
        }
        return false;
    }

    private bool isUnseen(D_PointObject pointScript)
    {
        return !pointScript.isSeen()         // return true if point has not been seen
               || pointScript.ignoreSeen     // return true if point is ignoring seen
                || ignoreSeen;               // return true if scrips is ignoring seen
    }
    
    //==============================================================
    //===================== Spawn Health ===========================
    //==============================================================
    // does what it says
    private bool spawn(GameObject point)
    {
        point.GetComponent<D_PointObject>().trigger();
        director.Debug().Log("Sent command to spawn " + spawnType.ToString());
        return true;
    }
}
