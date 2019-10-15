using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class D_Debug
{

    private static D_DirectorObjects director;

    public D_Debug()
    {
    }

    public D_Debug(D_DirectorObjects d)
    {
        director = d;
    }

    public void Log(string message)
    {
        if (director.isDebug())
        {
            Debug.Log(message);
        }
    }
}
