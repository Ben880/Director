using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class D_Flags: MonoBehaviour
{
    [System.Serializable]//makes sure this shows up in the inspector
    public class Flag {
        public Flag(string k, bool b)
        {
            key = k;
            value = b;
        }
        public string key;//your name variable to edit
        public bool value = false;//bool value
    }    
    [SerializeField]
    private List<Flag> flags = new List<Flag>();

    public int getFlagId(string s)
    {
        for (int i = 0; i < flags.Count; i++)
        {
            if (flags[i].key == s)
                return i;
        }
        flags.Add(new Flag(s, false));
        return flags.Count-1;
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
    
    public void setValue(string s, bool b)
    {
        flags[getFlagId(s)].value = b;
    }
    
    public void toggleValue(int i)
    {
        flags[i].value = !(flags[i].value);
    }
}
