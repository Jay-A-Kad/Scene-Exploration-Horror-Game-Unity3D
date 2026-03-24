using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player movment
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public bool isWalking = false;
    CharacterController PlayerController;

    // player look around
    public Transform Playerbody;
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    public float yRoatation = 0f;

    void Start()
    {
        PlayerController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //player movement
        float speed = isWalking ? walkingSpeed : runningSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        PlayerController.Move(move * speed * Time.deltaTime);

        //player look around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //playe look up and down
        xRotation -= mouseY;
        yRoatation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //apply rotation to the camera
        transform.localRotation = Quaternion.Euler(0, yRoatation, 0f); // for looking up and down set the xrotatin 
        //player look left and right
        Playerbody.Rotate(Vector3.up * mouseX);

    }
}
