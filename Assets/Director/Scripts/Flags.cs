using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Flags: MonoBehaviour
{
    [System.Serializable]//makes sure this shows up in the inspector
    public class Flag {
        public string key;//your name variable to edit
        public bool value = false;//bool value
    }    
    [SerializeField]
    private Flag[] flags = new Flag[1];

    public int getFlagId(string s)
    {
        for (int i = 0; i < flags.Length; i++)
        {
            if (flags[i].key == s)
                return i;
            
        }

        return -1;
    }

    public bool getValue(int i)
    {
        return flags[i].value;
    }
    
    public bool getValue(string s)
    {
        return flags[getFlagId(s)].value;
    }

    public void setValue(int i, bool b)
    {
        flags[i].value = b;
    }
    
    public void toggleValue(int i)
    {
        flags[i].value = !(flags[i].value);
    }
}
