using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class D_SpawnRandom : D_PointObject
{

    public bool canSpawnMultipleTimes = false;
    public GameObject[] spawnObjects = new GameObject[1];


    public override void trigger()
    {
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length-1)], gameObject.transform.position, gameObject.transform.rotation);
        if (!canSpawnMultipleTimes)
            spawnable = false;
    }
    
}

/*
     //==================================
    //======  Respawn variables  =======
    //==================================
    [System.Serializable]
    public class Respawning
    {
        public bool allowRespawn = false;
        public float minRespawnTime = 0;
        public float maxRespawnTime = 0;
        public int maxRespawns = 5;
    }
    public Respawning respawning;
 
 
 
 [CustomEditor(typeof(D_SpawnRandom))]
public class ScriptEditor : Editor
{
    public void OnInspectorGUI()
    {
        var myScript = target as D_SpawnRandom;
 
        myScript.allowRespawn = EditorGUILayout.Toggle("AllowRespawn", myScript.allowRespawn);
 
        using (var group = new EditorGUILayout.FadeGroupScope(10))
        {
            if (group.visible == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("minRespawnTime");
                myScript.minRespawnTime = EditorGUILayout.FloatField(myScript.minRespawnTime);
                EditorGUILayout.PrefixLabel("maxRespawnTime");
                myScript.maxRespawnTime = EditorGUILayout.FloatField(myScript.maxRespawnTime);
                EditorGUILayout.PrefixLabel("maxRespawns");
                myScript.maxRespawns = EditorGUILayout.IntField(myScript.maxRespawns);
                EditorGUI.indentLevel--;
            }
        }
 
        //myScript.allowRespawn = GUILayout.Toggle(myScript.allowRespawn, "AllowRespawn");
 
        using (new EditorGUI.DisabledScope(myScript.allowRespawn))
        {
            myScript.minRespawnTime = EditorGUILayout.FloatField("minRespawnTime", myScript.maxRespawnTime);
            myScript.maxRespawnTime = EditorGUILayout.FloatField("maxRespawnTime", myScript.maxRespawnTime);
            myScript.maxRespawns = EditorGUILayout.IntField("maxRespawns", myScript.maxRespawns);
        }
    }
}*/