using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_TriggerObject : D_DirectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            director.Debug().Log("Zone update");
        }
    }
}
