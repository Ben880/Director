using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_SpawnHazzard : D_LogicObject
{

    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (director.getData().getFloat("Health").value > 60 && director.getData().getFloat("Health").value < 90)
        {
            gameObject.SetActive(true);
        }
    }
}
