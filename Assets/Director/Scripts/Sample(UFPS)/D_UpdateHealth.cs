using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class D_UpdateHealth : D_InputObject
{
    
    private vp_FPPlayerEventHandler m_Player = null;

    private int healthIndex;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_Player = m_DDirector.getPlayer().GetComponentInChildren<vp_FPPlayerEventHandler>();
        healthIndex = m_DDirector.getData().getFloatIndex("Health");
    }

    // Update is called once per frame
    void Update()
    {
        m_DDirector.getData().getFloat(healthIndex).value = m_Player.Health.Get()*10f;
    }
}
