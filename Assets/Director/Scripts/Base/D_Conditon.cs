using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

    public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(dataName);
        switch (condition)
        {
            case conditionType.greaterThan:
                sb.Append(" > ");
                break;
            case conditionType.lessThan:
                sb.Append(" < ");
                break;
            case conditionType.equal:
                sb.Append(" == ");
                break;
            case conditionType.notEqual:
                sb.Append(" != ");
                break;
            case conditionType.greaterThanOrEqual:
                sb.Append(" >= ");
                break;
            case conditionType.lessThanOrEqual:
                sb.Append(" <= ");
                break;
        }
        sb.Append(otherData);
        return sb.ToString();
    }
}
