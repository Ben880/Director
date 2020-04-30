using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAdder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ct = GetComponent<CommandTracker>();
        Command cmd = new Command("test", true);
        ct.registerCommand(cmd, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
