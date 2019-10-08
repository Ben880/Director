using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdateHealth : InputObject
{
    
    private vp_FPPlayerEventHandler m_Player = null;

    private int healthIndex;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_Player = director.getPlayer().GetComponentInChildren<vp_FPPlayerEventHandler>();
        healthIndex = director.getData().getFloatIndex("Health");
    }

    // Update is called once per frame
    void Update()
    {
        director.getData().getFloat(healthIndex).value = m_Player.Health.Get()*10f;
    }
}
