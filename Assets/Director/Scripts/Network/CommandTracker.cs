using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DirectorProtobuf;
using UnityEngine;
[RequireComponent(typeof(ServerConnection))]
[RequireComponent(typeof(ProtoRouter))]
public class CommandTracker: Routable 
{
    // ===========================================================================================
    // Purpose: tracks commands and their changes and sends to server
    // ===========================================================================================
    private Dictionary<string, Command> commands = new Dictionary<string, Command>();
    private ServerConnection serverConnection;
    // ======================================================================
    // ==============================Unity Functions=========================
    // ======================================================================
    public void Awake()
    {
        serverConnection = GetComponent<ServerConnection>();
        GetComponent<ProtoRouter>().RegisterRoute(DataWrapper.MsgOneofCase.ExecuteCommand, this);
    }
    // ======================================================================
    // ==============================Override Functions======================
    // ======================================================================
    public override void Route(DataWrapper wrapper) { ReceivedCommand(wrapper.ExecuteCommand); }
    // ======================================================================
    // ==============================Public Functions========================
    // ======================================================================
    public void AddNotifyObject(string key, NotifyObject notifyObj) { GetCommand(key).AddNotifyObject(notifyObj); }

    public void NewSession() { foreach (var cmd in commands) { SendToServer(cmd.Value); } }
    
    public void RegisterCommand(Command command, bool send)
    {
        commands.Add(command.GetName(), command);
        if (send)
            SendToServer(command);
    }
    
    public void SetCommandEnabled(string key, bool value)
    {
        GetCommand(key).SetEnabled(value);
        SendToServer(GetCommand(key));
    }
    
    public void ReceivedCommand(ExecuteCommand ec)
    {
        if (commands.ContainsKey(ec.Name))
        {
            Command commandToExecute = commands[ec.Name];
            if (commandToExecute == null)
                Debug.LogError("Command to execute is null");
            else if (!commandToExecute.IsEnabled())
                Debug.LogError("Command to execute is disabled");
            else
            {
                commandToExecute.Execute();
            }
        }
    }
    // ======================================================================
    // ==============================Private Functions========================
    // ======================================================================
    private void SendToServer(Command command)
    {
        CommandChange commandBuff = new CommandChange();
        commandBuff.Name = command.GetName();
        commandBuff.Value = command.IsEnabled();
        DataWrapper wrapper = new DataWrapper();
        wrapper.CommandChange = commandBuff;
        serverConnection.SendToServer(wrapper);
    }
    
    private Command GetCommand(string key)
    {
        if (commands.ContainsKey(key))
            return commands[key];
        else
            Debug.LogError($"Key ({key}) does not exist", this);
        throw new Exception("No such key");
    }
    
    // ======================================================================
    // ==============================Test Functions==========================
    // ======================================================================
    public bool CommandExists(string commandName) { return commands.ContainsKey(commandName); }
}
