using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CommandTracker
{
    
    Dictionary<string, Command> commands = new Dictionary<string, Command>();
    
    public void registerCommand(string s)
    {
        registerCommand(new Command(s, false));
    }

    public void registerCommand(Command command)
    {
        commands.Add(command.getName(), command);
        PacketObject po = new PacketObject();
        po.setDestination("ServerCommandTracker");
        po.setCommand("RegisterCommand");
        po.addNode(new PacketNode("CommandName", command.getName()));
        po.addNode(new PacketNode("Enabled", command.isEnabled()));
    }

    public Command getCommand(string s)
    {
        return commands[s];
    }

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
}
