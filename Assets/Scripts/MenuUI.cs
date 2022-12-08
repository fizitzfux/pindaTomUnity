using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // VARIABLES
    private bool MenuOpen = false;


    // REFERENCES
    public GameObject MenuParent;
    public Behaviour CameraController;
    public Behaviour PlayerController;

    private void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu key pressed");
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
    }

    private void OpenMenu()
    {
        ChangeCursorState(false);
        MenuOpen = true;
        MenuParent.SetActive(true);
        PlayerController.enabled = false;
        CameraController.enabled = false;
    }

    public void CloseMenu()
    {
        ChangeCursorState(true);
        MenuOpen = false;
        MenuParent.SetActive(false);
        PlayerController.enabled = true;
        CameraController.enabled = true;
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
