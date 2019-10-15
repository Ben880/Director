using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class D_Points
{
    private static GameObject startPoint;
    private static GameObject endPoint;

    private static List<GameObject> healthPoints = new List<GameObject>();
    private static List<GameObject> itemPoints = new List<GameObject>();
    private static List<GameObject> obstaclePoints = new List<GameObject>();
    private static List<GameObject> hazzardPoints = new List<GameObject>();
    private static List<GameObject> enemyPoints = new List<GameObject>();
    private static bool init = false;

    public D_Points()
    {
        if (!init)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HealthPoint"))
            {
                healthPoints.Add(obj);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ItemPoint"))
            {
                itemPoints.Add(obj);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ObstaclePoint"))
            {
                obstaclePoints.Add(obj);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HazardPoint"))
            {
                hazzardPoints.Add(obj);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EnemyPoint"))
            {
                enemyPoints.Add(obj);
            }
            startPoint = GameObject.FindGameObjectWithTag("StartPoint");
            endPoint = GameObject.FindGameObjectWithTag("EndPoint");
            init = true;
            Debug.Log("Points found:" +
                      "\nStart Point: " + startPoint +
                      "\nEnd Point: " + endPoint +
                      "\nHealthPoints: " + healthPoints.Count +
                      "\nItemPoints: " + itemPoints.Count +
                      "\nObstaclePoints: " + obstaclePoints.Count +
                      "\nHazardPoints: " + hazzardPoints.Count +
                      "\nEnemyPoints: " + enemyPoints.Count );
        }
    }

    public List<GameObject> getHealth()
    {
        return healthPoints;
    }
    public List<GameObject> getItem()
    {
        return itemPoints;
    }
    public List<GameObject> getObstacle()
    {
        return obstaclePoints;
    }
    public List<GameObject> getHazard()
    {
        return hazzardPoints;
    }
    public List<GameObject> getEnemy()
    {
        return enemyPoints;
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
        List<GameObject> points = new List<GameObject>();
        foreach (var point in healthPoints)
        {
            points.Add(point);
        }
        foreach (var point in enemyPoints)
        {
            points.Add(point);
        }
        foreach (var point in hazzardPoints)
        {
            points.Add(point);
        }
        foreach (var point in itemPoints)
        {
            points.Add(point);
        }
        foreach (var point in obstaclePoints)
        {
            points.Add(point);
        }
        points.Add(startPoint);
        points.Add(endPoint);
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
