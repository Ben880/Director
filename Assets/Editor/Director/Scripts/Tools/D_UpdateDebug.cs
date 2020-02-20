using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_UpdateDebug : MonoBehaviour
{
    private D_DirectorObjects director;
    public GameObject textPrefab;
    private TextMeshProUGUI textMeshComponent;


    // Start is called before the first frame update
    void Start()
    {
        director = new D_DirectorObjects();
        textMeshComponent = textPrefab.GetComponent<TextMeshProUGUI>();
    }



    // Update is called once per frame
    void Update()
    {
        textMeshComponent.text = director.getData().ToString();
    }

}
