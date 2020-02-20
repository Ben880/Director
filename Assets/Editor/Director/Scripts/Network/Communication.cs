using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;

[RequireComponent(typeof(CommandTracker))]
public class Communication : MonoBehaviour
{
    //other needed classes
    private NetworkSettings ns;
    private CommandTracker commandTracker;
    // networking classes
    private TcpClient socketConnection; 	
    private Thread clientReceiveThread;


    public bool debug = true;
    public bool silenceAllConsoleMessages = false;

    void Awake()
    {
        // get needed classes
        ns = new NetworkSettings(this);
        commandTracker = GetComponent<CommandTracker>();
        // connect to the server
        connectToServer();
        if (silenceAllConsoleMessages)
            Debug.Log("Errors are being hidden");
    }
    
    private void connectToServer()
    {
        try {  			
            clientReceiveThread = new Thread (new ThreadStart(ListenForCommands)); 			
            clientReceiveThread.IsBackground = true; 			
            clientReceiveThread.Start();  		
        } 		
        catch (Exception e) { 			
            sendMessageToConsole("connect exception " + e, true); 		
        } 	

    }

    public bool sendToServer(PacketObject packetObject) // function to send packets
    {
        if (socketConnection == null) {             
            return false;         
        }  		
        try { 			
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream(); 			
            if (stream.CanWrite) {
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(packetObject.ToString()); 				
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
                sendMessageToConsole("Client sent his message - should be received by server", false);             
            }         
        } 		
        catch (SocketException socketException) {             
            sendMessageToConsole("Socket exception: " + socketException, true);         
        }     
        return false;
    }



    private void ListenForCommands()
    {
        try { 			
            socketConnection = new TcpClient("localhost", 8052);
            Byte[] bytes = new Byte[1024];             
            while (true) { 				
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream()) { 					
                    int length; 					
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
                        var incommingData = new byte[length]; 						
                        Array.Copy(bytes, 0, incommingData, 0, length); 						
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData); 	
                        sendMessageToConsole("message received: " +serverMessage, false);
                        decodeCommand(serverMessage);
                    } 				
                } 			
            }         
        }         
        catch (SocketException socketException) {             
            sendMessageToConsole("Socket exception: " + socketException, true);         
        }     

    }

    public void sendMessageToConsole(string s, bool overrideDebug)
    {
        if (!silenceAllConsoleMessages && (debug || overrideDebug))
        {
            Debug.Log("Network: " + s);
        }
    }

    private void decodeCommand(string s)
    {
        PacketObject po = new PacketObject();
        po.parseString(s);
        //add logic deciding where the command should be relayed to
        if (po.getDestination().Equals("Commands"))
        {
            commandTracker.recievedCommand(po);	
        }
        else if (po.getDestination().Equals("UnityMain"))
        {
            //if (po.getCommand().Equals("setID"))//<==============
                //ns.ID = Int32.Parse(po.getNode(po.findIndexOfNodeWithKey("ID")).value);
        }
        		
    }

    void OnApplicationQuit()
    {
        PacketObject po = new PacketObject();
        po.setDestination("ServerMain");
        po.setCommand("EndConnection");
        po.addNode(new PacketNode("Reason", "ApplicationQuit"));
        //sendToServer(po);
        
        socketConnection.Close();
        clientReceiveThread.Abort();
        Debug.Log("Network:;;;; " );
    }

}
