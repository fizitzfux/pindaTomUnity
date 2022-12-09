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
        normalWalkSpeed = this.Player.GetComponent<playerController>().walkSpeed;
        normalRunSpeed = this.Player.GetComponent<playerController>().runSpeed;
        normalMouseSensitivity = this.Camera.GetComponent<CameraController>().mouseSensitivity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu toggled");

            if(MenuOpen)
            {
                //close inventory
                CloseMenu();
            }
            else
            {
                //open inventory
                OpenMenu();
            }
        }
        else if (MenuOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        ChangeCursorState(false);
        MenuOpen = true;
        MenuParent.SetActive(true);

        this.Player.GetComponent<playerController>().walkSpeed = 0;
        this.Player.GetComponent<playerController>().runSpeed = 0;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = 0;
    }

    public void CloseMenu()
    {
        ChangeCursorState(true);
        MenuOpen = false;
        MenuParent.SetActive(false);

        this.Player.GetComponent<playerController>().walkSpeed = normalWalkSpeed;
        this.Player.GetComponent<playerController>().runSpeed = normalRunSpeed;
        this.Camera.GetComponent<CameraController>().mouseSensitivity = normalMouseSensitivity;
    }

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
