using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DirectorProtobuf;
using Google.Protobuf;

[RequireComponent(typeof(CommandTracker))]
[RequireComponent(typeof(ProtoRouter))]
[RequireComponent(typeof(NetworkSettings))]
public class ServerConnection : MonoBehaviour
{
    // ===========================================================================================
    // Purpose: handles connection to server
    // ===========================================================================================
    //other needed classes
    private CommandTracker commandTracker;
    private ProtoRouter protoRouter;
    // networking classes
    private TcpClient socketConnection; 	
    private Thread clientReceiveThread;
    private NetworkSettings networkSettings;
    // timer
    private float timer = 0;
    // ======================================================================
    // ==============================Unity Functions=========================
    // ======================================================================
    public void Awake()
    {
        commandTracker = GetComponent<CommandTracker>();
        networkSettings = GetComponent<NetworkSettings>();
        protoRouter = GetComponent<ProtoRouter>();
        ConnectToServer();
    }

    public void Update()
    {
        if (socketConnection != null) return;
        timer += Time.deltaTime;
        if (!(timer > 2)) return;
        ConnectToServer();
        timer = 0;
    }

    public void OnApplicationQuit()
    {
        clientReceiveThread.Abort();
        socketConnection.Close();
    }
    // ======================================================================
    // ==============================Unity Functions=========================
    // ======================================================================
    private void ConnectToServer()
    {
        try 
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForCommands)) {IsBackground = true};
            clientReceiveThread.Start();
        } 		
        catch (Exception e) { Debug.Log("connect exception " + e, this); }
    }

    public void SendToServer(DataWrapper wrapper)
    {
        if (socketConnection == null || !socketConnection.Connected)            
            return;
        try
        {
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite) {
                wrapper.WriteDelimitedTo(stream);
            }         
        } 		
        catch (SocketException socketE) { Debug.Log("Socket exception: " + socketE, this); }
    }
    private void ListenForCommands()
    {
        try
        {
            socketConnection = new TcpClient(NetworkConfig.Host, NetworkConfig.Port);
            commandTracker.NewSession();
            DataWrapper wrapper = new DataWrapper();
            while (true)
            {
                var stream = socketConnection.GetStream();
                do
                {
                    wrapper.MergeDelimitedFrom(stream);
                } while (stream.DataAvailable);
                Debug.Log("message received: " + wrapper.MsgCase.ToString(), this);
                protoRouter.RouteProtobuf(wrapper);
            }
        }
        catch (SocketException socketE) { Debug.Log("Socket exception: " + socketE, this); }
        finally
        {
            socketConnection = null;
            clientReceiveThread.Abort();
        }
    }
    

}
