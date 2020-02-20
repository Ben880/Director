using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_RegiserConsoleTextMesh : D_DirectorObject
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        director.Debug().setConsole(gameObject.GetComponent<TextMeshProUGUI>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
