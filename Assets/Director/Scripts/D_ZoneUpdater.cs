using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ZoneUpdater : MonoBehaviour
{
    private DirectorObjects director;
    // Start is called before the first frame update
    void Start()
    {
        director = new DirectorObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            director.setCurrentZone(gameObject);
            Debug.Log("Zone update");
        }
    }
}
