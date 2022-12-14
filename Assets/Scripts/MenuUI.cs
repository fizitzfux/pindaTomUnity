using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // VARIABLES
    private bool MenuOpen = false;
    private float normalWalkSpeed;
    private float normalRunSpeed;
    private float normalMouseSensitivity;
    // REFERENCES
    public GameObject MenuParent;
    public GameObject Player;
    public GameObject Camera;

    private void Start()
    {
        // Store default states from Movement and Camera
        normalWalkSpeed = this.Player.GetComponent<playerController>().walkSpeed;
        normalRunSpeed = this.Player.GetComponent<playerController>().runSpeed;
        normalMouseSensitivity = this.Camera.GetComponent<CameraController>().mouseSensitivity;
    }

    private void Update()
    {
        // Toggle the menu UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MenuOpen)    CloseMenu();
            else            OpenMenu();
        }
        // If Menu open and Pressed E, close so Inventory can open
        else if (MenuOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseMenu();
        }
    }

    // Opens Menu and turns player movement off
    private void OpenMenu()
    {
        ChangeCursorState(false);
        MenuOpen = true;
        MenuParent.SetActive(true);

        this.Player.GetComponent<playerController>().walkSpeed = 0;
        this.Player.GetComponent<playerController>().runSpeed = 0;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = 0;
    }

    // Public so thet continue button can interact,
    // closes Menu and turns player movement back on
    public void CloseMenu()
    {
        ChangeCursorState(true);
        MenuOpen = false;
        MenuParent.SetActive(false);

        this.Player.GetComponent<playerController>().walkSpeed = normalWalkSpeed;
        this.Player.GetComponent<playerController>().runSpeed = normalRunSpeed;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = normalMouseSensitivity;
    }

    // Used for enabling and disabling cursor capture
    private void ChangeCursorState(bool lockCursor)
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
