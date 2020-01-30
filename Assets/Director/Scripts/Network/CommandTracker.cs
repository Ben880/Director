using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CommandTracker : MonoBehaviour
{
    
    Dictionary<string, Command> commands = new Dictionary<string, Command>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        
        po.getCommand();
        // other logic
    }
}
