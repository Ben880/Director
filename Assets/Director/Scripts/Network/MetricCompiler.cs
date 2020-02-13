using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
[RequireComponent(typeof(Communication))]
public class MetricCompiler : MonoBehaviour
{
    // ======================================================combination of send less and compute less
    //access to the data collection through D_DirectorObjects (Specifically .getData() method)
    private D_DirectorObjects director;
    //access to server communications
    private Communication communication;

    [SerializeField] 
    private float sendEveryMS = 100;
    private float counter = 0;


    void Awake()
    {
        communication = GetComponent<Communication>();
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= sendEveryMS)
        {
            sendData();
            counter = 0;
        }
    }

    private void sendData()
    {
        PacketObject packet = new PacketObject();
        packet.setDestination("MetricCompiler");
        packet.setCommand("UpdateData");
        foreach (var floatObj in director.getData().getFloatList())
        {
            packet.addNode(new PacketNode(floatObj.key, floatObj.value));
        }
        //send data string to the server
        communication.sendToServer(packet);
    }
}
