using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Lobby : NetworkBehaviour
{
    // Variables
    private int readyCount;
    private int playerCount;
    private int counter;
    private bool set = false;
    private Vector3 PlayerPosition;
    private ulong[] clientId = new ulong[1];
    public List<Vector3> spawnPositions;
    // References
    public Text stateText;
    public Text startText;
    public Text ipText;
    public GameObject player;
    public GameObject lobbyCanvas;


    void Start()
    {
        //Set user IP to display on screen
        ipText.text = NetworkConfig.localIp;
    }


    void Update()
    {
        // Get and display the amount of online players
        OnlinePlayerCountServerRpc();
        stateText.text = readyCount.ToString() + " / " + playerCount.ToString();
        // If R then say to server that player is ready
        if (!set && Input.GetKeyDown(KeyCode.R))
        {
            set = true;
            startText.text = "Started!";
            SetReadyServerRpc();
        }
    }

    // RPC to server with request for player count
    [ServerRpc(RequireOwnership = false)]
    private void OnlinePlayerCountServerRpc()
    {
        ReturnPlayerCountClientRpc(NetworkManager.ConnectedClients.Count);
    }
    // RPC from server to all clients with player count
    [ClientRpc]
    private void ReturnPlayerCountClientRpc(int clients)
    {
        playerCount = clients;
    }

    // RPC to server with request to add player to ready players
    [ServerRpc(RequireOwnership = false)]
    private void SetReadyServerRpc()
    {
        readyCount++;
        // If all players ready, start game
        if (readyCount >= playerCount)
        {
            GameStart();
        }
        // Else give all players updated ready count
        else ReturnReadyCountClientRpc(readyCount);
    }
    // RPC from server to all clients with ready count
    [ClientRpc]
    private void ReturnReadyCountClientRpc(int count)
    {
        readyCount = count;
    }

    // Via RPC to server, start the game
    private void GameStart()
    {
        // Get a random spawn coördinate from a predefined list for every player,
        // and Teleport all players to their coördinates
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            PlayerPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
            spawnPositions.Remove(PlayerPosition);
            // Get client ID
            clientId[0] = client.Key;
            ClientRpcParams rpcParams = default;
            rpcParams.Send.TargetClientIds = clientId;
            // Send the RPC to the client
            PlayerTeleportClientRpc(PlayerPosition, rpcParams);
        }
    }

    // RPC from server to specific client for teleport
    [ClientRpc]
    private void PlayerTeleportClientRpc(Vector3 pos, ClientRpcParams rpcParams = default)
    {
        player.GetComponent<playerController>().Teleport(pos);
        player.GetComponent<Health>().enabled = true;
        lobbyCanvas.SetActive(false);
    }
}
