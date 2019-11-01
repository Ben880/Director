using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[System.Serializable]
public class D_SpawnTypeConfiguration
{
    private Option[] options;
    
    public Option[] getOptions()
    {
        return options;
    }
    [System.Serializable]
    public class Option
    {
        [Header("Key")]
        public string key;
        //==============================================================
        //================= Spawn Quantity Rules =======================
        //==============================================================
        public enum quantityType
        {
            single, 
            batch,
            stream
        };
        [Header("Spawn Quantity Rules")]
        public quantityType spawnQuantity;
        
        
        
        //==============================================================
        //================= Spawn Location Rules =======================
        //==============================================================
        // determins what points should be considered 
        public enum locationType
        {
            range,
            random,
            zones
        };
        [Header("Spawn Location Rules")] public bool ignoreSeen = false;
        public locationType spawnLocation;
        public float minSpawnDistance = 5;
        public float maxSpawnDistance = 100;
        //==============================================================
        //================= Spawn Selection Rules ======================
        //==============================================================
        // determines which point to select from all points that were considered
        public enum selectionType
        {
            first,
            random,
            closestToPlayer,
            closestToPlayerAndEnd
        };
        [Header("Spawn Selection Rules")] public selectionType spawnSelection;
        //==============================================================
        //=================== Spawn Type Rules =========================
        //==============================================================
        // logic for determining what type of health should be spawned (if point supports it)
        public enum conditionType
        {
            Strict,
            Range,
            Random
        };
        [Header("Spawn Type Rules")] public conditionType spawnCondition;
        public float targetHealth = 70;
        public float forceSpawnThreshhold = 20;
        public float minRangeAmount = -10;
        public float maxRangeAmount = 10;
        //==============================================================
        //================= Spawn Frequency Rules ======================
        //==============================================================
        public enum frequencyType
        {
            Timer,
            WhleNeededWithTimer
        };
        [Header("Spawn Frequency Rules")] public frequencyType spawnFrequency;
        public float minTime = 10;
        public float maxTime = 100;

        public Option(string s)
        {
            key = s;
        }

    }

    public D_SpawnTypeConfiguration()
    {
        options = new Option[Enum.GetNames(typeof(spawnTypes)).Length];
        var values = spawnTypes.GetValues(typeof(spawnTypes));
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = new Option(values.GetValue(i).ToString());
        }
    }
}
