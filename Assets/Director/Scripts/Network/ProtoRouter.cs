using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class ProtoRouter : MonoBehaviour
{
    // ===========================================================================================
    // Purpose: route protobufs to appropriate location
    // ===========================================================================================
    private Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
    public void RouteProtobuf(DataWrapper wrapper)
    {
        if (routes.ContainsKey(wrapper.MsgCase))
            routes[wrapper.MsgCase].Route(wrapper);
        else
            Debug.Log("No route for:" + wrapper.MsgCase.ToString());
    }

    public void RegisterRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
    {
        routes.Add(buffName, routable);
    }
}
