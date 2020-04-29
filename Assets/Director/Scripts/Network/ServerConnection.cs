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
    //other needed classes
 
    private CommandTracker commandTracker;
    private ProtoRouter protoRouter;
    // networking classes
    private TcpClient socketConnection; 	
    private Thread clientReceiveThread;
    private bool retryConnection = false;
    private D_Timer retryTime = new D_Timer();
    
    void Awake()
    {
        commandTracker = GetComponent<CommandTracker>();
        protoRouter = GetComponent<ProtoRouter>();
        connectToServer();
    }

    void Start()
    {
        DataWrapper wrapper = new DataWrapper();
        wrapper.UnitySettings = new UnitySettings();
        wrapper.UnitySettings.Name = "Default";
        wrapper.UnitySettings.Public = true;
        sendToServer(wrapper);
    }

    private void Update()
    {
        if (retryConnection )
        {
            retryTime.step();
            if (retryTime.isCompleted())
            {
                connectToServer();
                retryTime.set(1);
            }
        }
    }

    private void connectToServer()
    {
        retryConnection = false;
        try {  			
            clientReceiveThread = new Thread (new ThreadStart(ListenForCommands)); 			
            clientReceiveThread.IsBackground = true; 			
            clientReceiveThread.Start();
        } 		
        catch (Exception e) { 			
            Debug.Log("connect exception " + e, this); 		
        }
    }

    public void sendToServer(DataWrapper protoObject)
    {
        if (socketConnection == null || !socketConnection.Connected) {             
            return;         
        }  
        try
        {
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite) {
                protoObject.WriteDelimitedTo(stream);
            }         
        } 		
        catch (SocketException socketException) {             
            Debug.Log("Socket exception: " + socketException, this);         
        }
    }
    private void ListenForCommands()
    {
        try 
        { 			
            socketConnection = new TcpClient(NetworkConfig.host, NetworkConfig.port);
            DataWrapper wrapper = new DataWrapper();          
            while (true)
            {
                var stream = socketConnection.GetStream();
                do
                {
                    wrapper.MergeDelimitedFrom(stream);
                } while (stream.DataAvailable);
                Debug.Log("message received: " +wrapper.MsgCase.ToString(), this);
                protoRouter.routeProtobuf(wrapper);
            }         
        }         
        catch (SocketException socketException) {             
            Debug.Log("Socket exception: " + socketException, this);
            retryConnection = true;
            retryTime.set(1);
            clientReceiveThread.Abort();
        }
    }
    
    void OnApplicationQuit()
    {
        clientReceiveThread.Abort();
        socketConnection.Close();
    }
}
