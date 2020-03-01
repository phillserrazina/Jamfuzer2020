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

    private AudioManager audioManager;
    [SerializeField] private float stepTimer = 0.75f;
    private float currentStepTimer;

    // EXECUTION METHODS

    private void Start() {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        audioManager = FindObjectOfType<AudioManager>();
        currentStepTimer = 0f;
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
        mouseX = (Input.GetAxis("Mouse X") + Input.GetAxis("Controller Look Around X")) * mouseSensitivityX;
        mouseY = (Input.GetAxis("Mouse Y") + Input.GetAxis("Controller Look Around Y")) * mouseSensitivityY;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
    }

    private void Move() {
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (horDirection != 0 || verDirection != 0) {
            if (currentStepTimer > 0) {
                currentStepTimer -= Time.deltaTime;
                goto Movement;
            }
            
            currentStepTimer = stepTimer;
            audioManager.Play("Step", 2);
        }
        else {
            currentStepTimer = 0;
        }

        Movement:

        rb.MovePosition(transform.position + 
                        (transform.forward * verDirection * movementSpeed * Time.fixedDeltaTime) + 
                        (transform.right * horDirection * movementSpeed * Time.fixedDeltaTime));
    }
}
