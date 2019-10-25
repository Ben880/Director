using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine;

public class D_Data: MonoBehaviour
{
    //====================================
    //=============Floats=================
    //====================================
    [System.Serializable]
    public class Floats {
        public Floats(string s)
        {
            key = s;
        }
        public string key;
        public float value = 0;
    }
    [SerializeField]
    private List<Floats> floats = new List<Floats>();
    //====================================
    //=============Ints===================
    //====================================
    [System.Serializable]
    public class Ints {
        public string key;
        public int value = 0;//bool value
    }    
    [SerializeField]
    private Ints[] ints = new Ints[1];

    public void Start()
    {

    }
    
    

    public List<Floats> getFloatList()
    {
        return floats;
    }
    public void addFloat(string s)
    {
        floats.Add(new Floats(s));
    }
    
    public Floats getFloat(int i)
    {
        return floats[i];
    }

    public Floats getFloat(string s)
    {
        return floats[getFloatIndex(s)];
    }

    public int getFloatIndex(string s)
    {
        for (int i = 0; i < floats.Count; i++)
        {
            if (string.Equals(floats[i].key, s))
                return i;
        }

        addFloat(s);
        return floats.Count - 1;
    }
    
    public Ints getInt(int i)
    {
        return ints[i];
    }

    public int getIntIndex(string s)
    {
        for (int i = 0; i < ints.Length; i++)
        {
            if (string.Equals(ints[i].key, s))
                return i;
        }

        return -1;
    }
    

}
