using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class PacketNode
{
    public string key;
    public dynamic value;

    public override string ToString()
    {
        return new StringBuilder().Append(key).Append(":").Append(value).ToString();
    }
    // test for passing stupid shit
    public PacketNode(string key, dynamic value)
    {
        this.key = key;
        this.value = value;
    }
}
