using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_LookingAtDebug : MonoBehaviour
{
    public float interactionDistance = 20;
    private D_DirectorObjects _dDirector;
    public GameObject textPrefab;
    public GameObject mainCamera;

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        _dDirector = new D_DirectorObjects();
        text = textPrefab.GetComponent<TextMeshProUGUI>();
        if (mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward) * interactionDistance;
        //debuging lets you see the ray cast up to unit of 10
        if (_dDirector.getFlags().getValue("Debug"))
        {
            
            Debug.DrawRay( mainCamera.transform.position, forward, Color.green);
        }

        RaycastHit hit;
        //checks to see if the raycast hits
        if (Physics.Raycast(mainCamera.transform.position, forward, out hit))
        {
            GameObject lookingAt = hit.collider.gameObject; //gets the object the raycast hit
            string tag = lookingAt.tag;
            if (tag.Equals("HealthPoint") || tag.Equals("ItemPoint")|| tag.Equals("ObstaclePoint")|| tag.Equals("EnemyPoint")|| tag.Equals("HazardPoint")) // checks if object is tagged as interactable
            {
                if (hit.distance < interactionDistance)
                {
                    try
                    {
                        text.text = lookingAt.GetComponent<D_PointObject>().getDebugText();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Debug.Log("Point has no D_PointObject child script. Removing it.");
                        GameObject.Destroy(hit.collider.gameObject);
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
