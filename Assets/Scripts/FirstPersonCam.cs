using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] float xSensitivity, ySensitivity;
    [SerializeField] Transform orientation, cameraPos;

    public bool canLook;
    float xRotation, yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canLook = true;
    }

    private void Update()
    {
        if (canLook)
        {
            // mouse input
            float mouseX = (Input.GetAxisRaw("Mouse X") * xSensitivity);
            float mouseY = (Input.GetAxisRaw("Mouse Y") * ySensitivity);

            yRotation += mouseX;
            xRotation -= mouseY;

            // prevent breaking your neck
            xRotation = Mathf.Clamp(xRotation, -90f, 90);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}