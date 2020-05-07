using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

[RequireComponent(typeof(ServerConnection))]
public class CommandRequester : MonoBehaviour
{
    // ===========================================================================================
    // Purpose: request a command from the server to space out received time between commands
    // ===========================================================================================
    [SerializeField]
    private float requestTime = 0.2f;
    private float timer = 0;
    private ServerConnection sc;

    public void Awake() { sc = GetComponent<ServerConnection>();}

    public void Update()
    {
        timer += Time.deltaTime;
        if (!(timer > requestTime)) return;
        DataWrapper wrapper = new DataWrapper {GetCommand = new GetCommand()};
        sc.SendToServer(wrapper);
        timer = 0;
    }
}
