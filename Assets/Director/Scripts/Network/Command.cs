using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    private string commandName = "";
    private bool enabled = false;
    private List<NotifyObject> notifyObjects = new List<NotifyObject>();

    // constructor that sets all needed data
    public Command(string name, bool enabled)
    {
        commandName = name;
        this.enabled = enabled;
    }
    
    // get the name of the command
    public string getName()
    {
        return commandName;
    }
    public bool isEnabled()
    {
        return enabled;
    }
    public void setEnabled(bool enabled)
    {
        this.enabled = enabled;
        
    }

    public void registerNotifyObject(NotifyObject notifyObject)
    {
        notifyObjects.Add(notifyObject);
    }

    public void execute()
    {
        foreach (NotifyObject notifyObj in notifyObjects)
        {
            notifyObj.notify();
        }
    }
    
}
