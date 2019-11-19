﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
[System.Serializable]
public class D_Conditon
{
    private static D_DirectorObjects director;
    private static bool init = false;
    public enum conditionType
    {
        greaterThan,
        greaterThanOrEqual,
        lessThan,
        lessThanOrEqual,
        equal,
        notEqual
    };

    public string dataName = "";
    public conditionType condition;
    public string otherData;
    //public bool randomize = false;
    //public float randomOffset = 0;
    private bool doNumberTest = true;
    private bool isNumber = false;
    private float value;
    
    public bool evaluate()
    {
        if (!init)
        {
            director = new D_DirectorObjects();
            init = true;
        }

        if (doNumberTest)
        {
            isNumber = float.TryParse(otherData, out value);
            doNumberTest = false;
        }
        if (!isNumber)
        {
            value = director.getData().getFloat(otherData).value;
        }
        switch (condition)
        {
            case conditionType.greaterThan:
                return director.getData().getFloat(dataName).value > value;
            case conditionType.lessThan:
                return director.getData().getFloat(dataName).value < value;
            case conditionType.equal:
                return director.getData().getFloat(dataName).value == value;
            case conditionType.notEqual:
                return director.getData().getFloat(dataName).value != value;
            case conditionType.greaterThanOrEqual:
                return director.getData().getFloat(dataName).value >= value;
            case conditionType.lessThanOrEqual:
                return director.getData().getFloat(dataName).value <= value;
            default:
                return false;
        }

    }
}
