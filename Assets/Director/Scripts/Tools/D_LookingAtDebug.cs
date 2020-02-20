using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_LookingAtDebug : D_DirectorObject
{
    //public editor variables
    public float interactionDistance = 60;
    //private variables
    private GameObject mainCamera;
    private TextMeshProUGUI text;
    private Vector3 forward;
    private string lookingAtTag;
    private GameObject lookingAt;
    void Start()
    {
        base.Start();
        text = gameObject.GetComponent<TextMeshProUGUI>();
        mainCamera = director.getMainCamera();
    }

    // Update is called once per frame
    void Update()
    {
        forward = mainCamera.transform.TransformDirection(Vector3.forward) * interactionDistance;
        //debuging lets you see the ray cast up to unit of 10
        if (isDebug())
        {
            Debug.DrawRay( mainCamera.transform.position, forward, Color.green);
        }
        RaycastHit hit;
        //checks to see if the raycast hits
        if (Physics.Raycast(mainCamera.transform.position, forward, out hit))
        {
            //gets the object the raycast hit
            lookingAt = hit.collider.gameObject; 
            lookingAtTag = lookingAt.tag;
            // checks if object is tagged as point object
            if (lookingAtTag.Equals("PointObject")) 
            {
                if (hit.distance < interactionDistance)
                {
                    try
                    {
                        text.text = lookingAt.GetComponent<D_PointObject>().getDebugText();
                    }
                    catch (Exception e)
                    {
                        director.Debug().Log("D_LookingAtDebug: Point has no D_PointObject child script.");
                    }
                }
            }
            else
            {
                text.text = "";
            }
        }
        
        
    }
}
