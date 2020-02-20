using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class D_EnableMeshOnDebug : D_DirectorObject
{
    
    private MeshRenderer mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mesh.enabled = director.isDebug();

    }
}
