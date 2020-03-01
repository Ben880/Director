using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class NetworkSettings
{
    private string id = "";
    private bool publicSession = true;
    private Communication communication;

    public NetworkSettings(Communication communication)
    {
        this.communication = communication;
    }

    public string ID
    {
        get { return id; }
        set
        {
            id = value;
            notifyServerOfChange("ID", value);
        }
    }

    public bool PublicSession
    {
        get { return publicSession; }
        set
        {
            publicSession = value;
            notifyServerOfChange("PublicSession", value.ToString());
        }
    }

    private void notifyServerOfChange(string key, string value)
    {
        PacketObject packet = new PacketObject();
        packet.setDestination("NetworkSettings");
        packet.setCommand("ChangeSetting");
        packet.addNode(new PacketNode(key, value));
        Data data = new Data();
        //communication.sendToServer(packet);
    }
}
