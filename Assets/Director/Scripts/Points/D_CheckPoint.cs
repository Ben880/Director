using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CheckPoint : D_DirectorObject
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            director.Debug().Log("player entered");
            new D_DirectorObjects().getDirectorObject().GetComponentInChildren<D_UpdateTime>().applyCheckpoint(gameObject);
            enabled = false;
        }
    }
}
