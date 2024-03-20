using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] float xSensitivity, ySensitivity;
    [SerializeField] Transform orientation, cameraPos;

    public bool canLook;
    public LayerMask teleportLayer;
    public GameObject teleportMarker;
    
    float xRotation, yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canLook = true;
        teleportMarker.SetActive(false);
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

        RaycastHit hit;
        if (Input.GetKey(KeyCode.LeftShift) && 
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, teleportLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            teleportMarker.SetActive(true);
            teleportMarker.transform.position = hit.point;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) &&
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, teleportLayer))
        {
            Vector3 telePos = hit.point;
            transform.position = new Vector3(telePos.x, transform.position.y, telePos.z);
            teleportMarker.SetActive(false);
        }
    }

    public void Teleport()
    {

    }
}