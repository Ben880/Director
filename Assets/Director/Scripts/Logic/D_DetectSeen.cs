using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class D_DetectSeen : D_LogicObject
{
    Collider objCollider;
    Camera cam;
    Plane[] planes;
    private List<GameObject> pointObjects;
    private bool logMessage = true;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cam = director.getMainCamera().GetComponent<Camera>();
        pointObjects = director.getPoints().getAll();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            planes = GeometryUtility.CalculateFrustumPlanes(cam);
            foreach (var point in pointObjects)
            {
                if (GeometryUtility.TestPlanesAABB(planes, point.GetComponent<Collider>().bounds))
                {
                    Vector3 heading =  (point.transform.position - director.getMainCamera().transform.position).normalized;
                    RaycastHit hit;
                    if (Physics.Raycast(director.getMainCamera().transform.position, heading, out hit))
                    {
                        D_PointObject pointScript = hit.transform.GetComponent<D_PointObject>();
                        Debug.DrawRay(director.getMainCamera().transform.position, heading * hit.distance, Color.yellow);
                        try
                        {
                            pointScript.setSeen(true);
                        }
                        catch (Exception e)
                        {
                            if (logMessage && director.isDebug())
                            {
                                director.Debug().Log("Empty catch statement: may be masking error");
                                logMessage = false;
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            pointObjects = director.getPoints().getAll();
        }
    }


}
