using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class Communication : MonoBehaviour
{
    private TcpClient socketConnection; 	
    private Thread clientReceiveThread;
    private bool connected = false;     //this may not be needed
    private string id;                    //id of the Unity Client
    private CommandTracker commandTracker;
    public bool debug = true;
    public bool silenceAllConsoleMessages = false;

    void Awake()
    {
        connectToServer();
        commandTracker = gameObject.GetComponent<CommandTracker>();
        if (silenceAllConsoleMessages)
        {
            Debug.Log("Errors are being hidden");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sendToServer("");
    }

    private void connectToServer() //gets an id from the server and then calls reconnect
    {
        //connect logic
        string connectResponse = "key"; // logic for getting id
        id = connectResponse; //gets the key combine with above ========****####*#*#*#*#
        try {  			
            clientReceiveThread = new Thread (new ThreadStart(ListenForCommands)); 			
            clientReceiveThread.IsBackground = true; 			
            clientReceiveThread.Start();  		
        } 		
        catch (Exception e) { 			
            sendMessageToConsole("connect exception " + e, true); 		
        } 	

    }

    public bool sendToServer(string clientMessage) // function to send packets
    {
        if (socketConnection == null) {             
            return false;         
        }  		
        try { 			
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream(); 			
            if (stream.CanWrite) {
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage); 				
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
            if (po.getCommand().Equals("setID"))
                id = Int32.Parse(po.getNode(po.findIndexOfNodeWithKey("ID")).value);
        }
        		
    }

    void OnApplicationQuit()
    {
        PacketObject po = new PacketObject();
        po.setDestination("ServerMain");
        po.setCommand("EndConnection");
        po.addNode(new PacketNode("Reason", "ApplicationQuit"));
        sendToServer(po.ToString());
        clientReceiveThread.Abort();
        socketConnection.Close();
    }

}
