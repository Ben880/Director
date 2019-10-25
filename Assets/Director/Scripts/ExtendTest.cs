using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendTest : Test
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Debug.Log("Extend");
    }

    public void test()
    {
        Debug.Log("Extend Test");
    }
    
}
