using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder(1000);
        foreach (var val in floats)
        {
            sb.Append(val.key);
            sb.Append(": ");
            sb.Append(val.value);
            sb.Append("\n");
        }
        return sb.ToString();
    }
}
