using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using UnityEngine;

public class ProtoRouter : MonoBehaviour
{
    private Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
    public void routeProtobuf(DataWrapper wrapper)
    {
        routes[wrapper.MsgCase].route(wrapper);
    }

    public void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
    {
        routes.Add(buffName, routable);
    }
}
