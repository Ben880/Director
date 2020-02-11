using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ProBuilder2.Common;
using UnityEngine;

public class PacketObject
{
    private string destination;
    private string command; 
    //protobuff 
    //--or Json newtonsoft
    private List<PacketNode> nodes = new List<PacketNode>();

    public void setDestination(string destination)
    {
        this.destination = destination;
    }

    public string getDestination()
    {
        return destination;
    }
    
    public void setCommand(string command)
    {
        this.command = command;
    }

    public string getCommand()
    {
        return command;
    }

    public void addNode(PacketNode node)
    {
        nodes.Add(node);
    }

    public int findIndexOfNodeWithKey(string keyName)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].key.Equals(keyName))
                return i;
        }
        return -1;
    }

    public PacketNode getNode(int index)
    {
        return nodes[index];
    }

    public int numberOfNodes()
    {
        return nodes.Count;
    }

    public string toString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Destination:");
        sb.Append(destination);
        sb.Append(",Command:");
        sb.Append(command);
        foreach (var node in nodes)
        {
            sb.Append(",");
            sb.Append(node.ToString());
        }
        return sb.ToString();
    }

    public void parseString(string s)
    {
        string[] strings = s.Split(',');
        foreach (var str in strings)
        {
            string[] splitNode = str.Split(':');
            if (splitNode[0].Contains("Destination"))
            {
                destination = splitNode[1];
            }
            else if (splitNode[0].Contains("Command"))
            {
                command = splitNode[1];
            }
            else
            {
                nodes.Add(new PacketNode(splitNode[0], splitNode[1]));
            }
        }
    }
    
    
}
