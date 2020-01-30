using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    //access to the data collection through D_DirectorObjects (Specifically .getData() method)
    private D_DirectorObjects director;
    //access to server communications
    private Communication communication;
    // Start is called before the first frame update
    void Start()
    {
        //obtaining refrences to needed objects
        director = new D_DirectorObjects();
        communication = gameObject.GetComponent<Communication>();
    }

    // Update is called once per frame
    void Update()
    {
        //create a sting builder and iterate through data adding it to a string
        StringBuilder sb = new StringBuilder();
        foreach (var floatObj in director.getData().getFloatList())
        {
            sb.Append(floatObj.key);
            sb.Append(":");
            sb.Append(floatObj.value);
        }
        //send data string to the server
        communication.sendToServer(sb.ToString());
    }
}
