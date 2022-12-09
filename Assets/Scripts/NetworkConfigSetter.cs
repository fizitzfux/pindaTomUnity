using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkConfigSetter : MonoBehaviour
{
    private string ip;
    private ushort port;
    private bool client = true;
    private bool host = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIp(string Text)
    {
        ip = Text;
        Debug.Log("set NetworkConfig ip to " + ip);
        NetworkConfig.ip = ip;
    }

    public void SetPort(string Text)
    {
        port = (ushort)UInt16.Parse(Text);
        Debug.Log("set NetworkConfig port to " + Text);
        NetworkConfig.port = port;
    }

    public void ToggleClient()
    {
        client = !client;
        Debug.Log("set NetworkConfig client to " + client);
        NetworkConfig.Client = client;
    }

    public void ToggleHost()
    {
        host = !host;
        Debug.Log("set NetworkConfig host to " + host);
        NetworkConfig.Host = host;
    }
}

public static class NetworkConfig
{
    public static string ip = "127.0.0.1";
    public static ushort port = 6969;
    public static bool Client = false;
    public static bool Host = false;
    public static bool Server = false;
}