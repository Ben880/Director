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
    private NetworkSettings networkSettings;
    private float timmer = 0;
    
    void Awake()
    {
        commandTracker = GetComponent<CommandTracker>();
        networkSettings = GetComponent<NetworkSettings>();
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
        if (socketConnection == null)
        {
            timmer += Time.deltaTime;
            if (timmer > 2)
            {
                connectToServer();
                timmer = 0;
            }
        }
    }

    private void connectToServer()
    {
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
            networkSettings.newsession();
            commandTracker.newsession();
            DataWrapper wrapper = new DataWrapper();
            while (true)
            {
                var stream = socketConnection.GetStream();
                do
                {
                    wrapper.MergeDelimitedFrom(stream);
                } while (stream.DataAvailable);

                Debug.Log("message received: " + wrapper.MsgCase.ToString(), this);
                protoRouter.routeProtobuf(wrapper);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException, this);
        }
        finally
        {
            socketConnection = null;
            clientReceiveThread.Abort();
        }
    }
    
    void OnApplicationQuit()
    {
        clientReceiveThread.Abort();
        socketConnection.Close();
    }
}
