using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CommandTracker: MonoBehaviour 
{
    private Dictionary<string, Command> commands = new Dictionary<string, Command>();
    private Communication communication;
    void Awake()
    {
        communication = GetComponent<Communication>();
    }
    
    public void registerCommand(Command command)
    {
        commands.Add(command.getName(), command);
        PacketObject po = new PacketObject();
        po.setDestination("ServerCommandTracker");
        po.setCommand("RegisterCommand");
        po.addNode(new PacketNode("CommandName", command.getName()));
        po.addNode(new PacketNode("Enabled", command.isEnabled().ToString()));
    }
    
    //will be depreciated after protobuff
    public void recievedCommand(PacketObject po)
    {
        Command commandToExecute = commands[po.getCommand()];
        if (commandToExecute == null)
            Debug.LogError("Command to execute is null");
        else if (!commandToExecute.isEnabled())
            Debug.LogError("Command to execute is disabled");
        else
        {
            commands[po.getCommand()].execute();
        }
    }

    public void executeCommand(string nameOfCommandToExecute)
    {
        Command commandToExecute = commands[nameOfCommandToExecute];
        if (commandToExecute == null)
            Debug.LogError("Command to execute is null");
        else if (!commandToExecute.isEnabled())
            Debug.LogError("Command to execute is disabled");
        else
        {
            commandToExecute.execute();
        }
    }
    // ==================================================
    // ===methods for restricted access of commands======
    // ==================================================
    private Command getCommand(string key)
    {
        if (commands.ContainsKey(key))
            return commands[key];
        else
            Debug.LogError($"Key ({key}) does not exist", this);
        throw new Exception("No such key");
    }

    public void setCommandEnabled(string key, bool value)
    {
        getCommand(key).setEnabled(value);
        PacketObject packet = new PacketObject();
        packet.setDestination("CommandTracker");
        packet.setCommand("SetCommandEnabled");
        packet.addNode(new PacketNode("Key", key));
        packet.addNode(new PacketNode("Value", value.ToString()));
        communication.sendToServer(packet);
    }

    public void addNotifyObject(string key, NotifyObject notifyObj)
    {
        getCommand(key).registerNotifyObject(notifyObj);
    }

    public bool commandExists(string nameOfCommand)
    {
        return commands.ContainsKey(nameOfCommand);
    }
    
    
}
