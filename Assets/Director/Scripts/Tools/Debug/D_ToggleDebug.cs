using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ToggleDebug : MonoBehaviour
{
    // Start is called before the first frame update
    private int debugFlag;
    
    private D_Flags _dFlags;
    private D_DirectorObjects _dDirector;
    void Start()
    {
        _dDirector = new D_DirectorObjects();
        _dFlags = GameObject.FindGameObjectWithTag("Director").GetComponent<D_Flags>();
        debugFlag = _dDirector.getFlags().getFlagId("Debug");
    }

    // Update is called once per frame
    void Update()
    {
        //Keypad0
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _dFlags.toggleValue(debugFlag);
        }

    }
}
