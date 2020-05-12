using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DisplayConnectionInfo : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private NetworkSettings ns;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ns = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<NetworkSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Connection ID: ");
        sb.Append(ns.Name);
        sb.Append("\nPublicSession: ");
        sb.Append(ns.PublicSession);
        tmp.text = sb.ToString();
    }
}
