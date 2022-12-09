using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToggle : MonoBehaviour
{
    public GameObject DebugMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DebugMenu.SetActive(true);
        }
    }
}
