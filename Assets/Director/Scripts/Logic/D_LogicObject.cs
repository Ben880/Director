using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_LogicObject : D_DirectorObject
{
    [Header("Logic Object")]
    public bool execute = true;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    public virtual void executeLogic()
    {
        
    }

    public virtual bool doExecute()
    {
        return execute;
    }
}
