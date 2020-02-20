using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_DisableCanvasOnPlay : MonoBehaviour
{
    private int debugFlag;

    private Canvas canvas;
    private D_Flags _dFlags;
    // Start is called before the first frame update
    void Start()
    {
        _dFlags = GameObject.FindGameObjectWithTag("Director").GetComponent<D_Flags>();
        debugFlag = _dFlags.getFlagId("Debug");
        canvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.enabled = _dFlags.getValue(debugFlag);
    }
}
