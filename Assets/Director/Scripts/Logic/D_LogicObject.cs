using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_LogicObject : MonoBehaviour
{
    [Header("Logic Base")]
    public bool execute = true;
    protected DirectorObjects director;
    // Start is called before the first frame update
    protected void Start()
    {
        director = new DirectorObjects();
    }

    public virtual void executeLogic()
    {
        
    }

    public virtual bool doExecute()
    {
        return execute;
    }
}
