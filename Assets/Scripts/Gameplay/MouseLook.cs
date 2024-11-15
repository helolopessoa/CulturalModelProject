﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 400f;
    public Transform playerBody;
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        //yRotation = Mathf.Clamp(90f, yRotation, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, -xRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //playerBody.Rotate(Vector3.left * mouseX);

    }
}


