using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEditor;


public class D_Director : MonoBehaviour
{

    private D_DirectorObjects directorObjects;
    public enum styles
    {
        flat,
        linear,
        exponential,
        decay,
        climax,
        random
    };

    public styles style;
    public float styleFactor = 1;
    
    public enum difficulties
    {
        easy,
        medium,
        hard,
        extreme,
        extremex,
        extremexx
    }

    public difficulties difficulty;
    

    [SerializeField]
    public D_SpawnTypeConfiguration.Option[] spawnConfigs = new D_SpawnTypeConfiguration().getOptions();
    

    //public AwakeExample t = new AwakeExample();
    
    public void Awake()
    {
        directorObjects = new D_DirectorObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}





