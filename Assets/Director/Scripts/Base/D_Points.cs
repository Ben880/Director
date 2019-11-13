using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class D_Points
{
    private static GameObject startPoint;
    private static GameObject endPoint;

    private static List<GameObject> points = new List<GameObject>();
    private static Dictionary<string, List<GameObject>> pointTypes = new Dictionary<string, List<GameObject>>();

    
    private static bool init = false;

    public D_Points()
    {
        if (!init)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PointObject"))
            {
                points.Add(obj);
            }
            var values = spawnTypes.GetValues(typeof(spawnTypes));
            foreach (var val in values)
            {
                pointTypes.Add(val.ToString(), new List<GameObject>());
            }
            foreach (GameObject obj in points)
            {
                pointTypes[obj.GetComponent<D_PointObject>().spawnType.ToString()].Add(obj);
            }
            startPoint = GameObject.FindGameObjectWithTag("StartPoint");
            endPoint = GameObject.FindGameObjectWithTag("EndPoint");
            init = true;
            Debug.Log("Points found:" + points.Count + 
                      "\nStart Point: " + startPoint +
                      "\nEnd Point: " + endPoint);
        }
    }

    public void removePoint(spawnTypes type, GameObject obj)
    {
        pointTypes[type.ToString()].Remove(obj);
    }

    public List<GameObject> getPointsOfType(spawnTypes type)
    {
        return pointTypes[type.ToString()];
    }

   
    public GameObject getStart()
    {
        return startPoint;
    }
    public GameObject getEnd()
    {
        return endPoint;
    }

    public List<GameObject> getAll()
    {
        return points;
    }

    public List<GameObject> getZonePoints(List<GameObject> points, GameObject currentZone)
    {
        List<GameObject> pointsInZone = new List<GameObject>();
        foreach (GameObject point in points)
        {
            if (currentZone.GetComponent<Collider>().bounds.Intersects(point.GetComponent<Collider>().bounds))
            {
                pointsInZone.Add(point);
            }
        }

        return pointsInZone;
    }

    public void zoneUpdate(GameObject currentZone)
    {
        Debug.Log("D_Points zoneUpdate");
        foreach (GameObject point in getAll())
        {
            D_PointObject pointObject = point.GetComponent<D_PointObject>();
                if (pointObject != null)
                    pointObject.updateZone(currentZone);
        }
    }
    

    
    
}
