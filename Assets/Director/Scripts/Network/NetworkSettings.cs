using System;
using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

[RequireComponent(typeof(ServerConnection))]
[RequireComponent(typeof(ProtoRouter))]
public class NetworkSettings: Routable
{
    private string name = "";
    private bool publicSession = true;
    private ServerConnection serverConnection;

    void  Awake()
    {
        serverConnection = GetComponent<ServerConnection>();
        GetComponent<ProtoRouter>().registerRoute(DataWrapper.MsgOneofCase.UnitySettings, this);
    }

    void Start()
    {
        notifyServerOfChange();
    }

    public override void route(DataWrapper wrapper)
    {
        name = wrapper.UnitySettings.Name;
    }

    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            notifyServerOfChange();
        }
    }

    public bool PublicSession
    {
        get { return publicSession; }
        set
        {
            publicSession = value;
            notifyServerOfChange();
        }
    }

    private void notifyServerOfChange()
    {
        DataWrapper wrapper = new DataWrapper();
        wrapper.UnitySettings = new UnitySettings();
        wrapper.UnitySettings.Name = name;
        wrapper.UnitySettings.Public = publicSession;
        serverConnection.sendToServer(wrapper);
    }
}
