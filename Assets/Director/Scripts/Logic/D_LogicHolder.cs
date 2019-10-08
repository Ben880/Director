using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_LogicHolder : MonoBehaviour
{
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "Locates LogicObjects attached to gameObject and its children objects.";
    private List<Component> logicHolder = new List<Component>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (D_LogicObject decision in gameObject.GetComponents<D_LogicObject>())
        {
            logicHolder.Add(decision);
        }
        foreach (var trigger in gameObject.GetComponentsInChildren<D_LogicObject>())
        {
            logicHolder.Add(trigger);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (D_LogicObject logic in logicHolder)
        {
            if (logic.doExecute())
                logic.executeLogic();
        }
    }
}
