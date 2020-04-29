using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class ProtoRouter : MonoBehaviour
{
    private Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
    public void routeProtobuf(DataWrapper wrapper)
    {
        if (routes.ContainsKey(wrapper.MsgCase))
            routes[wrapper.MsgCase].route(wrapper);
        else
        {
            Debug.Log("No route for:" + wrapper.MsgCase.ToString());
        }
    }

    public void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
    {
        routes.Add(buffName, routable);
    }
}
