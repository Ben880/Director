using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Triggers : MonoBehaviour
{
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "Locates TriggerObjects attached to gameObject and its children objects. Triggers should be used to trigger a logic object.";
    
    private static List<D_TriggerObject> triggers = new List<D_TriggerObject>();

    void Start()
    {
        foreach (var trigger in gameObject.GetComponents<D_TriggerObject>())
        {
            triggers.Add(trigger);
        }

        foreach (var trigger in gameObject.GetComponentsInChildren<D_TriggerObject>())
        {
            triggers.Add(trigger);
        }
    }
    

}
