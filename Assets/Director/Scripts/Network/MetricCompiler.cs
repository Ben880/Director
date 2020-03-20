using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DirectorProtobuf;
using UnityEngine;
[RequireComponent(typeof(ServerConnection))]
public class MetricCompiler : MonoBehaviour
{
    //access to the data collection through D_DirectorObjects (Specifically .getData() method)
    private D_DirectorObjects director;
    private ServerConnection serverConnection;

    [SerializeField] 
    private float sendEveryMS = 100;
    private float counter = 0;
    
    void Awake()
    {
        director = new D_DirectorObjects();
        serverConnection = GetComponent<ServerConnection>();
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
        foreach (var floatObj in director.getData().getFloatList())
        {
            Data data = new Data();
            data.Name = floatObj.key;
            data.Value = floatObj.value;
            dataList.Data.Add(data);
        }
        DataWrapper wrapper = new DataWrapper();
        wrapper.DataList = dataList;
        serverConnection.sendToServer(wrapper);
    }
}
