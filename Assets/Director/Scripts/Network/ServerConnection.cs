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
public class ServerConnection : MonoBehaviour
{
    //other needed classes
    private NetworkSettings ns;
    private CommandTracker commandTracker;
    private ProtoRouter protoRouter;
    // networking classes
    private TcpClient socketConnection; 	
    private Thread clientReceiveThread;

    void Awake()
    {
        ns = new NetworkSettings(this);
        commandTracker = GetComponent<CommandTracker>();
        protoRouter = GetComponent<ProtoRouter>();
        connectToServer();
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
        if (socketConnection == null || !ns.useNetwork) {             
            return;         
        }  
        try {
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
        try { 			
            socketConnection = new TcpClient(NetworkConfig.host, NetworkConfig.port);
            DataWrapper wrapper = new DataWrapper();          
            while (true)
            {
                var stream = socketConnection.GetStream();
                do
                {
                    wrapper.MergeDelimitedFrom(stream);
                } while (stream.DataAvailable);
                Debug.Log("message received: " +wrapper.ToString(), this);
                protoRouter.routeProtobuf(wrapper);
            }         
        }         
        catch (SocketException socketException) {             
            Debug.Log("Socket exception: " + socketException, this);         
        }     

    }
    
    void OnApplicationQuit()
    {
        clientReceiveThread.Abort();
        socketConnection.Close();
    }
}
