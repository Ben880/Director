using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ZoneUpdater : MonoBehaviour
{
    private D_DirectorObjects _dDirector;
    // Start is called before the first frame update
    void Start()
    {
        _dDirector = new D_DirectorObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _dDirector.setCurrentZone(gameObject);
            Debug.Log("Zone update");
        }
    }
}
