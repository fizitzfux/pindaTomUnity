using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkConfigHandler : MonoBehaviour
{
    public GameObject NetworkManagerObject;
    // Start is called before the first frame update
    void Start()
    {
        NetworkManagerObject.GetComponent<UnityTransport>().ConnectionData.Address = NetworkConfig.ip;
        NetworkManagerObject.GetComponent<UnityTransport>().ConnectionData.Port = NetworkConfig.port;
        Debug.Log("Netvars set");
        Debug.Log("Attempting to start...");
        if (NetworkConfig.Host) NetworkManager.Singleton.StartHost();
        else if (NetworkConfig.Server) NetworkManager.Singleton.StartServer();
        #if UNITY_EDITOR
            Debug.Log("Started Host because of editor");
            NetworkManager.Singleton.StartHost();
        #endif
        NetworkManager.Singleton.StartClient();
    }
    void Awake()
    {

    }
}
