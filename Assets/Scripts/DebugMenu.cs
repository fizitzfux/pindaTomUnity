// Allowed to be messy and undocumented, is for debug purposes ~Jip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    // References
    public GameObject menu;
    public Text ipText;
    [SerializeField] private Button ServerButton;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    // Variables
    private bool menuOpen = false;

    void Start()
    {
        ipText.text = NetworkConfig.localIp;
    }

    void Update()
    {
        // Toggle the menu
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!menuOpen)
            {
                menu.SetActive(true);
                menuOpen = true;
            }
            else
            {
                menu.SetActive(false);
                menuOpen = false;
            }
        }
    }

    private void Awake()
    {
        ServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        HostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        ClientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
