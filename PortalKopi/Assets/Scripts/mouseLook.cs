using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    //Sensitivity of the mouse movement
    public float mouseSensitivity = 100;

    //Reference to the Transform of the player
    public Transform playerCap;

    //initial rotation around the x-axis
    private float xRotation = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor at the center of the screen to hide it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse input for both x and y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        
        //Prevents the camera from flipping
        xRotation = Math.Clamp(xRotation, -90f, 90f);
        
        //Applying the rotation to the local rotation of the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Rotate the player's main transform around the Y-axis based on the X-axis mouse input
        playerCap.Rotate(Vector3.up*mouseX);
    }
}
