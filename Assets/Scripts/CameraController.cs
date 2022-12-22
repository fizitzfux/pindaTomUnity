using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    //VARIABLES
    [SerializeField] public float mouseSensitivity;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraLocation;
    
    
    //REFERENCES
    float xRotation = 0f;

    void Start()
    {
        // Zorgt ervoor dat je cursor niet zichtbaar is in de playmode
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Zet elke frame de camera op een geselecteerde transform
        transform.position = cameraLocation.position;
        // Zorgt ervoor dat je de camera kan draaien door de muis te bewegen op de Y en X assen
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Zorgt ervoor dat je maar 90 graden omhoog en 90 graden omlaag kan kijken
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Zorgt dat je beweegt vanaf je eigen rotatie ipv wereld rotatie zodat je kijk richting bepaald hoe je bewegingn plaatsvind
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        transform.position = cameraLocation.position;
    }
}
