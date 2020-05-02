using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class CommandRequester : MonoBehaviour
{
    [SerializeField]
    private float requestTime = 5;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > requestTime)
        {
            //Debug.Log("Requesting any avalible commands",this);
            DataWrapper wrapper = new DataWrapper();
            wrapper.GetCommand = new GetCommand();
            gameObject.GetComponent<ServerConnection>().sendToServer(wrapper);
            timer = 0;
        }
    }
}
