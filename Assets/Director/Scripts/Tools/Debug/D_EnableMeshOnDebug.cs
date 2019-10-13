using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class D_EnableMeshOnDebug : MonoBehaviour
{
    
    private int debugFlag;

    private MeshRenderer mesh;

    private D_Flags _dFlags;
    // Start is called before the first frame update
    void Start()
    {
        _dFlags = GameObject.FindGameObjectWithTag("Director").GetComponent<D_Flags>();
        debugFlag = _dFlags.getFlagId("Debug");
        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        mesh.enabled = _dFlags.getValue(_dFlags.getFlagId("Debug"));
        
    }
}
