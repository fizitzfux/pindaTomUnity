using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkCameraController : NetworkBehaviour
{
    public GameObject Camera;
    public Vector3 offset;

    void Start()
    {
        if (IsOwner)
        {
            Camera.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
