using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DirectorProtobuf;
using UnityEngine;
[RequireComponent(typeof(ServerConnection))]
public class CommandTracker: MonoBehaviour 
{
    private Dictionary<string, Command> commands = new Dictionary<string, Command>();
    private ServerConnection serverConnection;
    void Awake()
    {
        serverConnection = GetComponent<ServerConnection>();
    }

    public void registerCommand(Command command)
    {
        commands.Add(command.getName(), command);
        sendToServer(command);
    }
    public void setCommandEnabled(string key, bool value)
    {
        getCommand(key).setEnabled(value);
        sendToServer(getCommand(key));
    }
    //private logic for sending command info to server
    private void sendToServer(Command command)
    {
        CommandChange commandBuff = new CommandChange();
        commandBuff.Name = command.getName();
        commandBuff.Value = command.isEnabled();
        DataWrapper wrapper = new DataWrapper();
        wrapper.CommandChange = commandBuff;
        serverConnection.sendToServer(wrapper);
    }

    public void recievedCommand(DirectorRPC rpc)
    {
        Command commandToExecute = commands[rpc.Name];
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
    
    public void addNotifyObject(string key, NotifyObject notifyObj)
    {
        getCommand(key).addNotifyObject(notifyObj);
    }

    public bool commandExists(string commandName)
    {
        return commands.ContainsKey(commandName);
    }
    
}
