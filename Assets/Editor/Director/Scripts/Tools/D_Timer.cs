using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Timer
{

    private bool completed;
    private float timeLeft;
    
    public D_Timer(float time)
    {

        timeLeft = time;
    }
    
    public D_Timer()
    {

        
    }

    public bool isCompleted()
    {
        if (timeLeft < 0)
            return true;
        return false;
    }

    public void set(float time)
    {
        timeLeft = time;   
    }

    public void step()
    {
        if (timeLeft >0)
            timeLeft -= Time.deltaTime;
    }
    
    
    
 

}
