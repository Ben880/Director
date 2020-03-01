using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DirectorProtobuf;
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
        director = new D_DirectorObjects();
        communication = GetComponent<Communication>();
        Data data  = new Data();
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= sendEveryMS/1000)
        {
            sendData();
            counter = 0;
        }
    }

    private void sendData()
    {
        DataList dataList = new DataList();
        //packet.setDestination("MetricCompiler");
        //packet.setCommand("UpdateData");
        foreach (var floatObj in director.getData().getFloatList())
        {
            Data data = new Data();
            data.Name = floatObj.key;
            data.Value = floatObj.value;
            dataList.Data.Add(data);
            //packet.addNode(new PacketNode(floatObj.key, floatObj.value.ToString()));
        }
        //send data string to the server
        communication.sendToServer(dataList);
        Debug.Log("sending packet to communication moduel");
    }
}
