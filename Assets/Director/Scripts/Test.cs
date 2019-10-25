using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log("Base");
        test();
    }

    public void test()
    {
        Debug.Log("Base test");
    }
}
