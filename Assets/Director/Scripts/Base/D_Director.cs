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

    //============================
    //===== Custom Update ========
    //============================
    private float shortUpdateCounter = 0;
    private float longUpdateCounter = 0;
    private List<D_DirectorObject> objectsToUpdate = new List<D_DirectorObject>();
    //============================
    //===== Spawning =============
    //============================
    //public D_SpawnTypeConfiguration.Option[] spawnConfigs = new D_SpawnTypeConfiguration().getOptions();
    private static Dictionary<string, float> spawnChance = new Dictionary<string, float>();
    private D_SpawnController[] spawnControllers;
    //============================
    //===== Mono Functions========
    //============================
    public void Awake()
    {
        directorObjects = new D_DirectorObjects();
        getControllers();
        createSpawnData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTimers();
        foreach (var controller in spawnControllers)
        {
            controller.executeLogic();
        }
    }
    
    //============================
    //===== Call on Awake ========
    //============================
    private void getControllers()
    {
        spawnControllers = gameObject.GetComponentsInChildren<D_SpawnController>();
    }
    private void createSpawnData()
    {
        var values = spawnTypes.GetValues(typeof(spawnTypes));
        foreach (var val in values)
        {
            spawnChance.Add(val.ToString(), 0f);
        }
    }
    //============================
    //===== Update System ========
    //============================
    public void addToUpdateList(D_DirectorObject obj)
    {
        objectsToUpdate.Add(obj);
    }
    private void checkTimers()
    {
        shortUpdateCounter += Time.deltaTime;
        if (shortUpdateCounter > 1)
        {
            shortUpdateCounter = 0;
            longUpdateCounter++;
            foreach (var obj in objectsToUpdate)
            {
                obj.shortUpdate();
            }
        }
        if (longUpdateCounter > 5)
        {
            longUpdateCounter = 0;
            foreach (var obj in objectsToUpdate)
            {
                obj.longUpdate();
            }
        }
    }
    //============================
    //===== Spawn Systems ========
    //============================
    public void suggestDataChange()
    {
        
    }

    
    private void updateSpawnData()
    {
        
    }

    private void checkSpawnData()
    {
        
    }


}





