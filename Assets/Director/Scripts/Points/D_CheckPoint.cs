using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CheckPoint : MonoBehaviour
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
            Debug.Log("player entered");
            new D_DirectorObjects().getDirector().GetComponentInChildren<D_UpdateTime>().applyCheckpoint();
        }
    }
}
