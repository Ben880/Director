using System;
using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class NetworkSettings
{
    public bool useNetwork = true;
    private string id = "";
    private bool publicSession = true;
    private ServerConnection serverConnection;

    public NetworkSettings(ServerConnection serverConnection)
    {
        this.serverConnection = serverConnection;
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
        Data data = new Data();
        //communication.sendToServer(packet);
        throw new NotImplementedException();
    }
}
