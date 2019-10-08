using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ToggleDebug : MonoBehaviour
{
    // Start is called before the first frame update
    private int debugFlag;
    
    private Flags flags;
    private DirectorObjects director;
    void Start()
    {
        director = new DirectorObjects();
        flags = GameObject.FindGameObjectWithTag("Director").GetComponent<Flags>();
        debugFlag = director.getFlags().getFlagId("Debug");
    }

    // Update is called once per frame
    void Update()
    {
        //Keypad0
        if (Input.GetKeyDown(KeyCode.F1))
        {
            flags.toggleValue(debugFlag);
        }

    }
}
