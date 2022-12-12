using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public GameObject menu;
    public Text ipText;
    [SerializeField] private Button ServerButton;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;

    private bool menuOpen = false;

    void Start()
    {
        ipText.text = NetworkConfig.localIp;
    }

    void Update()
    {
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
