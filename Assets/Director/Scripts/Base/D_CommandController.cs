using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CommandController : D_DirectorObject
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
    //==============================================================
    //=================== Private ====================
    //==============================================================
    private bool loggedOutOfPoints = false;
    private List<string>  reasons = new List<string>();
    private int remainingPoints = 0;

    public string commandName = "Default";
    private NotifyObject no = new NotifyObject();
    private CommandTracker ct;
    private bool lastShouldSpawn = false;
    void Start()
    {
        foreach (var condition in conditions)
        {
            reasons.Add(condition.ToString());
        }
        Command command = new Command(commandName, false);
        command.addNotifyObject(no);
        ct = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<CommandTracker>();
        ct.registerCommand(command, true);
    }
    
    public void executeLogic()
    {
        if (shouldSpawn() != lastShouldSpawn)
        {
            ct.setCommandEnabled(commandName, shouldSpawn());
            lastShouldSpawn = shouldSpawn();
            Debug.Log("Changed command state " + commandName+ " to " + lastShouldSpawn, this);
        }
        listenForCommands();
    }


    public void listenForCommands()
    {
        if (no.isTriggered() && director.getDirectorScript().requestSpawn(spawnType))
        {
            no.reset();
            GameObject spawnObject = locateSpawn();                       // spawn result of locateHealth()
            if (spawnObject != null)
            {
                spawnObject.GetComponent<D_PointObject>().trigger();
                spawnObject.GetComponent<D_PointObject>().setSpawnReasons(gameObject.name, director.getData().getFloat("Total Time").value, reasons);
            }
            director.Debug().Log("SC-" +gameObject.name + " Located points: " + remainingPoints + "/" + director.getPoints().getPointsOfType(spawnType).Count+" type of: " + spawnType.ToString());
            director.getData().getFloat(spawnType.ToString() + " Spawned Last").value = 0;
            director.getDirectorScript().addSpawned(spawnType);
        }
        
    }
    //==============================================================
    //================= Should Spawn ===============================
    //==============================================================
    // method determines if the director should spawn health
    // current method is determined by health and timers
    // other methods may be determined by external factors/flags based on intensity, accuarcy and others
    // this will be done by checking for flags/values set by other classes
    private bool shouldSpawn()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].evaluate())
                return false;
        }
        return true;
    }
    //==============================================================
    //==================== Locate Health ===========================
    //==============================================================
    // determines which health to spawn from a list of health points from locateHealthSpawns()
    public GameObject locateSpawn()
    {
        //director.Debug().Log("D_SpawnController (" +gameObject.name+") locating spawn. Type: "  + spawnType.ToString());
        List<GameObject> points = locateSpawns();
        if (points.Count == 0)
        {
            Debug.Log("SC-" +gameObject.name +" Could not find any points to spawn. Type: "  + spawnType.ToString());
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

        remainingPoints = spawnablePoints.Count;
        if (spawnablePoints.Count == 0 && !loggedOutOfPoints)
        {
            director.Debug().Log("SC-" +gameObject.name + " out of points to spawn");
            loggedOutOfPoints = true;
        }
        remainingPoints = spawnablePoints.Count;
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
                || ignoreSeen;               // return true if scripts is ignoring seen
    }
    
}
