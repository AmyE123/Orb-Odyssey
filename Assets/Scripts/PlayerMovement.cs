using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to change the movement speed
    public float lookSpeed = 2f; // Adjust this to change the camera rotation speed
    public float maxVerticalAngle = 90f; // Maximum vertical angle for looking

    private float rotationX = 0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or left/right arrow keys
        float verticalInput = Input.GetAxis("Vertical"); // W/S or up/down arrow keys

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.Self);

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxVerticalAngle, maxVerticalAngle);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}


