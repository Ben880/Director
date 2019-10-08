using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvasOnPlay : MonoBehaviour
{
    private int debugFlag;

    private Canvas canvas;
    private Flags flags;
    // Start is called before the first frame update
    void Start()
    {
        flags = GameObject.FindGameObjectWithTag("Director").GetComponent<Flags>();
        debugFlag = flags.getFlagId("Debug");
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.enabled = flags.getValue(debugFlag);
    }
}
