using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private float mouseSensitivityY = 1f;
    [SerializeField] private float mouseSensitivityX = 4f;
    [SerializeField] private float movementSpeed = 5f;

    private float horDirection;
    private float verDirection;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    private Rigidbody rb;
    private Camera playerCamera;

    // EXECUTION METHODS

    private void Start() {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update () {
        GetInput();
    }

    private void FixedUpdate() {
        Move();
    }

    // METHODS

    private void GetInput() {
        horDirection = Input.GetAxis("Horizontal");
        verDirection = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
    }

    private void Move() {
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        rb.MovePosition(transform.position + 
                        (transform.forward * verDirection * movementSpeed * Time.fixedDeltaTime) + 
                        (transform.right * horDirection * movementSpeed * Time.fixedDeltaTime));
    }
}
