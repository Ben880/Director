using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class D_SpawnHealth : D_LogicObject
{
    public bool useUpdateInstead = true;
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
    public float minSpawnDistance = 5;
    public float maxSpawnDistance = 100;
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
    //=================== Spawn Type Rules =========================
    //==============================================================
    // logic for determining what type of health should be spawned (if point supports it)
    public enum conditionType 
    {
        Strict, 
        Range, 
        Random
    };
    [Header("Spawn Type Rules")] 
    public conditionType spawnCondition;
    public float targetHealth = 70;
    public float forceSpawnThreshhold = 20;
    public float minRangeAmount = -10;
    public float maxRangeAmount = 10;
    //==============================================================
    //================= Spawn Frequency Rules ======================
    //==============================================================
    public enum frequencyType
    {
        Timer, 
        WhleNeededWithTimer
    };
    [Header("Spawn Frequency Rules")] 
    public frequencyType spawnFrequency;
    public float minTime = 10;
    public float maxTime = 100;
    
    private int healthIndex;
    private float lastHealth;
    private float currentHealth;
    private bool doSpawnHealth = true;
    private D_Timer timer = new D_Timer();

    private bool standby = false;
    //need variable for this
    
    void Start()
    {
        base.Start();
        timer.set(10);
        healthIndex = director.getData().getFloatIndex("Health");
    }
    //==============================================================
    //==================== Execute Logic ===========================
    //==============================================================
    // main logic for decision system
    public override void executeLogic()
    {
        if (useUpdateInstead)
            return;
        base.executeLogic();                                                // execute parent method (should be empty)
        timer.step();                                                       // step the timer used for spawning
        currentHealth = director.getData().getFloat(healthIndex).value;     // update local variable
        if (shouldSpawnHelath())                                            // if shouldSpawnHelath()
        {
            GameObject spawnObject = locateHealthSpawn();                       // spawn result of locateHealth()
            if (spawnObject != null)
                spawnObject.GetComponent<D_PointObject>().trigger();
            float timerAmount = Random.Range(minTime, maxTime);
            timer.set(timerAmount);
            standby = false;
            if (director.isDebug())
                Debug.Log("Health Timer set at: " + timerAmount);
        }

        if (director.isDebug() && timer.isCompleted() && !standby)
        {
            Debug.Log("Health Timer complete entering standby mode");
            standby = true;
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
        return execute 
               && ((currentHealth < targetHealth && doSpawnHealth) 
                   || currentHealth < forceSpawnThreshhold)
               && frequencyCheck();
    }
    //==============================================================
    //================= Frequency Check  ===========================
    //==============================================================
    //checks to see if it is time to spawn health.
    private bool frequencyCheck()
    {
        bool isTimerCompleet = timer.isCompleted();
        if (spawnFrequency == frequencyType.Timer)
        {
            return isTimerCompleet;
        }
        if (spawnFrequency == frequencyType.WhleNeededWithTimer)
        {
            return isTimerCompleet && currentHealth < targetHealth;
        }
        //should not reach this point if so then spawn anyway
        return true;
    }
    //==============================================================
    //==================== Locate Health ===========================
    //==============================================================
    // determines which health to spawn from a list of health points from locateHealthSpawns()
    public GameObject locateHealthSpawn()
    {
        if (director.getFlags().getValue("Debug"))
            Debug.Log("locating health spawn");
        List<GameObject> points = locateHealthSpawns();
        if (points.Count == 0)
        {
            Debug.Log("D_SpawnHealth could not find any points to spawn");
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
    private List<GameObject> locateHealthSpawns()
    {
        List<GameObject> spawnablePoints = new List<GameObject>();
        foreach (GameObject point in director.getPoints().getPointsOfType(spawnTypes.Health))
        {
            if (isPointSpawnable(point))
            {
                spawnablePoints.Add(point);
            }
        }
        if (director.getFlags().getValue("Debug"))
            Debug.Log("Spawnable points found: " +
                      spawnablePoints.Count + "\nOut of: " +
                      director.getPoints().getPointsOfType(spawnTypes.Health).Count);
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
            return (distance >= minSpawnDistance && distance <= maxSpawnDistance) && isUnseen(pointScript);
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
    private bool spawnHealth(GameObject point)
    {
        point.GetComponent<D_PointObject>().trigger();
        if (director.getFlags().getValue("Debug"))
            Debug.Log("Sent command to spawn health");
        return true;
    }
    //==============================================================
    //===================== Old Code ===============================
    //==============================================================
    // this is odl code and should not be used
    void Update()
    {
        if (useUpdateInstead)
        {
            float health = director.getData().getFloat(healthIndex).value;
            if (health < 50f && health != 0)
            {
                if (lastHealth != health)
                    Debug.Log("Looking for locations to spawn health");
                lastHealth = health;
                foreach (GameObject point in director.getPoints().getPointsOfType(spawnTypes.Health))
                {
                    if (Vector3.Distance(director.getPlayer().transform.position, point.transform.position) <
                        maxSpawnDistance &&
                        point.GetComponent<D_PointObject>().isSpawnable())
                    {
                        point.GetComponent<D_PointObject>().trigger();
                        Debug.Log("Sent command to spawn health");
                    }
                }

            }
        }
    }
}
