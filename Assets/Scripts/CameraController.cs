using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    //VARIABLES
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraLocation;
    [SerializeField] private InventoryUI inventoryUI;
    
    
    //REFERENCES
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position = cameraLocation.position;
        //if(!inventoryUI.InventoryOpen)
        //{
            Rotation();
        //}

    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        transform.position = cameraLocation.position;
    }
}
