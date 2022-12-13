// Could use a over-do ~Jip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using AddressFamily = System.Net.Sockets.AddressFamily;

public class NetworkConfigSetter : MonoBehaviour
{
    // References
    [SerializeField] private Text localIp;
    // Variables
    private string ip;
    private ushort port;
    private bool client = true;
    private bool host = false;

    void Start()
    {
        // Get the local IP address and store it in the Network-wide config
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in hostEntry.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                NetworkConfig.localIp = ip.ToString();
                break;
            }
        }
    }

    // Public function for setting connection IP
    public void SetIp(string Text)
    {
        ip = Text;
        Debug.Log("set NetworkConfig ip to " + ip);
        NetworkConfig.ip = ip;
    }

    // Public function for setting connection Port
    public void SetPort(string Text)
    {
        port = (ushort)UInt16.Parse(Text);
        Debug.Log("set NetworkConfig port to " + Text);
        NetworkConfig.port = port;
    }

    // Public function for toggling Client
    public void ToggleClient()
    {
        client = !client;
        Debug.Log("set NetworkConfig client to " + client);
        NetworkConfig.Client = client;
    }

    // Public funtion for toggling Host feature-set
    public void ToggleHost()
    {
        host = !host;
        Debug.Log("set NetworkConfig host to " + host);
        NetworkConfig.Host = host;
        if (host) localIp.text = "IP: " + NetworkConfig.localIp;
        else localIp.text = "";
    }
}

// Public and static Network-wide available class with all necessary data
public static class NetworkConfig
{
    public static string ip = "127.0.0.1";
    public static ushort port = 6969;
    public static bool Client = false;
    public static bool Host = false;
    public static bool Server = false;
    public static string localIp = "offline";
}