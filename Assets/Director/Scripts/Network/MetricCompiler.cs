using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DirectorProtobuf;
using UnityEngine;
[RequireComponent(typeof(ServerConnection))]
public class MetricCompiler : MonoBehaviour
{
    // ===========================================================================================
    // Purpose: collects data and sends to server
    // access to the data collection through D_DirectorObjects (Specifically .getData() method)
    // ===========================================================================================
    private D_DirectorObjects director;
    private ServerConnection serverConnection;
    [SerializeField] 
    private const float SendEveryMs = 100;
    private float counter = 0;
    // ======================================================================
    // ==============================Unity Functions=========================
    // ======================================================================
    public void Awake()
    {
        director = new D_DirectorObjects();
        serverConnection = GetComponent<ServerConnection>();
        Data data  = new Data();
    }

    public void Update()
    {
        counter += Time.deltaTime;
        if (!(counter >= SendEveryMs / 1000)) return;
        SendData();
        counter = 0;
    }
    // ======================================================================
    // ==============================Private Functions=======================
    // ======================================================================
    public void SendData()
    {
        DataList dataList = new DataList();
        foreach (var floatObj in director.getData().getFloatList())
        {
            Data data = new Data {Name = floatObj.key, Value = floatObj.value};
            dataList.Data.Add(data);
        }
        DataWrapper wrapper = new DataWrapper {DataList = dataList};
        serverConnection.SendToServer(wrapper);
    }
}
