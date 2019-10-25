using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_DirectorObject : MonoBehaviour
{
    public static D_DirectorObjects director;
    //[Header("Direcotr Object")] 
    //public string scriptName = "";
    
    // Start is called before the first frame update
    protected void Start()
    {
        director = new D_DirectorObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void shortUpdate()
    {
        //1 second   
    }

    public virtual void longUpdate()
    {
        //5 seconds
    }
    
}
