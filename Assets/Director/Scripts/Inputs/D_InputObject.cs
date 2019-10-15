using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_InputObject : MonoBehaviour
{
    public bool enabled = true;
    
    protected D_DirectorObjects director;
    // Start is called before the first frame update
    protected void Start()
    {
        director = new D_DirectorObjects();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!enabled)
            return;
    }
}
