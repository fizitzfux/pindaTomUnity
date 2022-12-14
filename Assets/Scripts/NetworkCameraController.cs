// Literally one line, can probably be merged with something else ~Jip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkCameraController : NetworkBehaviour
{
    // References
    public GameObject Camera;

    void Start()
    {
        // Set camera active if client owns it
        if (IsOwner)    Camera.SetActive(true);
    }
}
