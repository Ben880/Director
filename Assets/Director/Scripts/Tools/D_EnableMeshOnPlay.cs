﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnableMeshOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
