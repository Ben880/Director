using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_DirectorObject : MonoBehaviour
{
    public static D_DirectorObjects director;

    
    // Start is called before the first frame update
    protected void Start()
    {

        if (director == null)
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

    public virtual float getDistanceToPlayer()
    {
        return Vector3.Distance(director.getPlayer().transform.position, gameObject.transform.position);
    }

    public virtual void trigger()
    {
        
    }

    public virtual void executeLogic()
    {
        
    }

    public virtual bool isOtherPlayer(GameObject other)
    {
        return other.CompareTag("Player");
    }

    public virtual bool isDebug()
    {
        return director.isDebug();
    }
}
