using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    // ===========================================================================================
    // Purpose: data type for commands
    // ===========================================================================================
    private string commandName = "";
    private bool enabled = false;
    private List<NotifyObject> notifyObjects = new List<NotifyObject>();

    // constructor that sets all needed data
    public Command(string name, bool enabled)
    {
        commandName = name;
        this.enabled = enabled;
    }
    
    // getters and setters
    public string GetName() { return commandName; }
    
    public bool IsEnabled() { return enabled; }
    
    public void SetEnabled(bool enabled) { this.enabled = enabled; }
    
    public void AddNotifyObject(NotifyObject notifyObject) { notifyObjects.Add(notifyObject); }

    public void Execute()
    {
        for (int i = 0; i < notifyObjects.Count; i++)
        {
            if (notifyObjects[i].Destroy)
                notifyObjects.RemoveAt(i);
            else
                notifyObjects[i].Notify();
        }
    }
    
}
