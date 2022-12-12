using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class SpawningArea : NetworkBehaviour
{
    private int readyCount;
    private int playerCount;
    private int counter;
    private bool set = false;

    public Text stateText;
    public Text startText;


    void Update()
    {
        if (!set && Input.GetKeyDown(KeyCode.R))
        {
            set = true;
            startText.text = "Started!";
            SetReadyServerRpc();
        }
            
        counter = 0;

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            counter++;
        }
        if (counter != playerCount)
        {
            playerCount = counter;
        }

        stateText.text = readyCount.ToString() + " / " + playerCount.ToString();
    }

    [ServerRpc]
    public void SetReadyServerRpc()
    {
        readyCount++;

        if (readyCount >= playerCount)
        {
            GameStart();
        }
    }

    private void GameStart()
    {
        //Moet nog geimplementeerd worden
        Debug.Log("start");
    }
}
