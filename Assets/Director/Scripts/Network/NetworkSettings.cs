using System;
using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

[RequireComponent(typeof(ServerConnection))]
[RequireComponent(typeof(ProtoRouter))]
public class NetworkSettings: Routable
{
    // ===========================================================================================
    // Purpose: tracks unity name and if its public and sends to server
    // ===========================================================================================
    private string name = "D";
    private bool publicSession = true;
    private ServerConnection serverConnection;
    // ======================================================================
    // ==============================Data Functions=========================
    // ======================================================================
    public string Name
    {
        get { return name; }
        set { 
            name = value; 
            NotifyServerOfChange(); 
        }
    }

    public bool PublicSession
    {
        get { return publicSession; }
        set
        {
            publicSession = value;
            NotifyServerOfChange();
        }
    }
    // ======================================================================
    // ==============================Unity Functions=========================
    // ======================================================================
    public void  Awake()
    {
        serverConnection = GetComponent<ServerConnection>();
        GetComponent<ProtoRouter>().RegisterRoute(DataWrapper.MsgOneofCase.UnitySettings, this);
    }

    public void Start()
    {
        NotifyServerOfChange();
    }
    // ======================================================================
    // ==============================override Functions======================
    // ======================================================================
    public override void Route(DataWrapper wrapper) { name = wrapper.UnitySettings.Name; }
    // ======================================================================
    // ==============================Public Functions========================
    // ======================================================================
    public void Newsession() { NotifyServerOfChange(); }
    
    private void NotifyServerOfChange()
    {
        DataWrapper wrapper = new DataWrapper();
        wrapper.UnitySettings = new UnitySettings {Name = name, Public = publicSession};
        serverConnection.SendToServer(wrapper);
    }
}
