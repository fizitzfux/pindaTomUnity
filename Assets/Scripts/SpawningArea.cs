using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class SpawningArea : NetworkBehaviour
{
    //Variables
    private int readyCount;
    private int playerCount;
    private int counter;
    private bool set = false;
    private Vector3 PlayerPosition;
    private ulong[] clientId = new ulong[1];
    public List<Vector3> spawnPositions;

    //references
    public Text stateText;
    public Text startText;
    public GameObject player;
    public GameObject lobbyCanvas;


    void Update()
    {
        OnlinePlayerCountServerRpc();
        stateText.text = readyCount.ToString() + " / " + playerCount.ToString();

        if (!set && Input.GetKeyDown(KeyCode.R))
        {
            set = true;
            startText.text = "Started!";
            SetReadyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void OnlinePlayerCountServerRpc()
    {
        ReturnPlayerCountClientRpc(NetworkManager.ConnectedClients.Count);
    }
    [ClientRpc]
    private void ReturnPlayerCountClientRpc(int clients)
    {
        playerCount = clients;
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetReadyServerRpc()
    {
        readyCount++;

        if (readyCount >= playerCount)
        {
            GameStart();
        }
        else ReturnReadyCountClientRpc(readyCount);
    }
    [ClientRpc]
    private void ReturnReadyCountClientRpc(int count)
    {
        readyCount = count;
    }


    private void GameStart()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            PlayerPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
            spawnPositions.Remove(PlayerPosition);

            clientId[0] = client.Key;
            ClientRpcParams rpcParams = default;
            rpcParams.Send.TargetClientIds = clientId;
            PlayerTeleportClientRpc(PlayerPosition, rpcParams);
        }
    }


    [ClientRpc]
    private void PlayerTeleportClientRpc(Vector3 pos, ClientRpcParams rpcParams = default)
    {
        player.GetComponent<playerController>().enabled = false;
        player.transform.position = pos;
        StartCoroutine(WaitBeforeEnable(0.1f));
    }

    private IEnumerator WaitBeforeEnable(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<playerController>().enabled = true;
        lobbyCanvas.SetActive(false);
    }
}
