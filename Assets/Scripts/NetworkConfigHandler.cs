using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkConfigHandler : MonoBehaviour
{
    public bool isServer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkConfig.Server) NetworkConfig.Server = isServer;

        // Set connection settings to configured settings
        GetComponent<UnityTransport>().ConnectionData.Address = NetworkConfig.ip;
        GetComponent<UnityTransport>().ConnectionData.Port = NetworkConfig.port;

        // Start Host, Server or Client depending on config
        if (NetworkConfig.Server) NetworkManager.Singleton.StartServer();
        else if (NetworkConfig.Host) NetworkManager.Singleton.StartHost();
        #if UNITY_EDITOR // If in unity editor start host, dev only
            Debug.Log("Started Host because of editor");
            NetworkManager.Singleton.StartHost();
        #endif
        NetworkManager.Singleton.StartClient();
    }
}
