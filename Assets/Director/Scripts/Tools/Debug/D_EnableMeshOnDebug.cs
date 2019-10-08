using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class D_EnableMeshOnDebug : MonoBehaviour
{
    
    private int debugFlag;

    private MeshRenderer mesh;

    private Flags flags;
    // Start is called before the first frame update
    void Start()
    {
        flags = GameObject.FindGameObjectWithTag("Director").GetComponent<Flags>();
        debugFlag = flags.getFlagId("Debug");
        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        mesh.enabled = flags.getValue(flags.getFlagId("Debug"));
        
    }
}
