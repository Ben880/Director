using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateDistance : D_InputObject
{
    private GameObject start;

    private GameObject end;
    

    private int totalIndex;
    private int traversedIndex;

    //private Data data;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        start = GameObject.FindGameObjectWithTag("StartPoint");
        end = GameObject.FindGameObjectWithTag("EndPoint");
        totalIndex = m_DDirector.getData().getFloatIndex("Total Distance");
        traversedIndex = m_DDirector.getData().getFloatIndex("Traversed Distance");
        m_DDirector.getData().getFloat(totalIndex).value = Vector3.Distance(start.transform.position, end.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        m_DDirector.getData().getFloat(traversedIndex).value = Vector3.Distance(start.transform.position, m_DDirector.getPlayer().transform.position);
    }
}
