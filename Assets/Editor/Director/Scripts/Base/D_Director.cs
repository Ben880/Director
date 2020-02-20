using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEditor;
using Random = System.Random;


public class D_Director : MonoBehaviour
{

    //==========================================================
    //====================== Director Difficulty ===============
    //==========================================================
    private D_DirectorObjects directorObjects;
    public enum styles
    {
        flat,
        linear,
        exponential,
        climax,
        compleetRandom,
        perlinNoiseWIP
    };

    public styles style;
    public float styleFactor = 1;
    
    public enum difficulties
    {
        easy = 1,
        medium = 2,
        hard = 3,
        extreme = 4,
        extremex = 5,
        extremexx = 6
    }
    public difficulties difficultyType;

    //private
    private float difficulty = 0;
    private float currentDifficulty = 0;
    
    
    //==========================================================
    //====================== Director States ===================
    //==========================================================
    [System.Serializable]
    public class State
    {
        public string name = "";
        public spawnTypes[] restrictTypes = new spawnTypes[0];
        public D_Conditon[] conditions = new D_Conditon[0];
        public bool isTypeRestricted(spawnTypes type)
        {
            return restrictTypes.Contains(type);
        }
        public bool state = false;
    }
    public State[] states = new State[0];
    
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
    private static float spawnChanceReductionCounter = 0;
    public static float spawnChanceReductionTime = 10; 
    private D_SpawnController[] spawnControllers;
    public int spawnChanceCount = 10; 
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
        defaultDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        checkTimers();
        foreach (var controller in spawnControllers)
        {
            controller.executeLogic();
        }
        updateStates();
        updateCurrentDifficulty();
        updateSpawnChance();
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
    //==========================================================
    //====================== Director Spawning =================
    //==========================================================
    public bool requestSpawn(spawnTypes type)
    {
        foreach (var state in states)
        {
            if (state.isTypeRestricted(type))
            {
                
                if (state.state)
                {
                    directorObjects.Debug().Log("Director refused spawn of " + type.ToString() + " reason " + state.name);
                    return false;
                }
            }
        }
        if (spawnChance[type.ToString()] >= spawnChanceCount)
        {
            directorObjects.Debug().Log("Director refused spawn of " + type.ToString() + " reason max spawn chance" );
            return false;
        }
        return true;
    }

    public void addSpawned(spawnTypes type)
    {
        spawnChance[type.ToString()] ++;
    }

    public void updateSpawnChance()
    {
        spawnChanceReductionCounter += Time.deltaTime * currentDifficulty;
        if (spawnChanceReductionCounter >= spawnChanceReductionTime)
        {
            var values = spawnTypes.GetValues(typeof(spawnTypes));
            foreach (var val in values)
            {
                if (spawnChance[val.ToString()] > 0) 
                    spawnChance[val.ToString()]--;
            }
        }
    }

    public void updateStates()
    {
        foreach (var state in states)
        {
            state.state = evaluateState(state);
            //directorObjects.getFlags().setValue(state.name, evaluateState(state));   
        }
    }

    public bool evaluateState(State state)
    {
        foreach (var condition in state.conditions)
        {
            if (!condition.evaluate())
                return false;
        }
        return true;
    }
    //==========================================================
    //====================== Director Difficulty================
    //==========================================================
    public void defaultDifficulty()
    {
        switch (difficultyType)
        {
            case difficulties.easy:
                difficulty = 1;
                break;
            case difficulties.medium:
                difficulty = 2;
                break;
            case difficulties.hard:
                difficulty = 3;
                break;
            case difficulties.extreme:
                difficulty = 4;
                break;
            case difficulties.extremex:
                difficulty = 5;
                break;
            case difficulties.extremexx:
                difficulty = 6;
                break;
        }
    }

    public void setDifficulty(float f)
    {
        difficulty = f;
    }

    public void updateCurrentDifficulty()
    {
        switch (style)
        {
            case styles.flat:
                currentDifficulty = difficulty * styleFactor;
                break;
            case styles.exponential:
                currentDifficulty += currentDifficulty * styleFactor * Time.deltaTime;
                if (currentDifficulty > difficulty)
                    currentDifficulty = difficulty;
                break;
            case styles.climax:
                currentDifficulty += currentDifficulty * styleFactor * Time.deltaTime;
                if (currentDifficulty > difficulty + 1)
                    currentDifficulty = 1;
                break;
            case styles.linear:
                currentDifficulty += styleFactor * Time.deltaTime;
                break;
            case styles.compleetRandom:
                currentDifficulty = UnityEngine.Random.Range(1, difficulty) * styleFactor;
                break;
            case styles.perlinNoiseWIP:
                break;
        }
    }

    public float getCurrentDifficulty()
    {
        return currentDifficulty;
    }

    public float getDiffifculty()
    {
        return difficulty;
    }




}





