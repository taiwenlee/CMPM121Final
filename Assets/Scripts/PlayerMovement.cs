using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public List<Camera> Cameras;
    private CharacterController characterController;
    private PlayerInput playerInput;

    public float speed = 5f;
    public float jumpSpeed;
    public float rotationSpeed;

    private float ySpeed;
    private float movementX;
    private float movementY;
    private float originalStepOffSet;
    private int index;
    private bool jumpPress = false;

    Vector3 forward;
    Vector3 right;

    private void Start()
    {
        EnableCamera(0);
        characterController = GetComponent<CharacterController>();
        originalStepOffSet = characterController.stepOffset;
    }
    private void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        movementX = move.x;
        movementY = move.y;
    }
    private void OnJump()
    {
        Debug.Log("Pressed jump");
        jumpPress = true; 
    }
    private void EnableCamera(int n)
    {
        Cameras.ForEach(cam => cam.enabled = false);
        Cameras[n].enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableCamera(0);
            index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableCamera(1);
            index = 1;
        }
        forward = Cameras[index].transform.forward;
        right = Cameras[index].transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        // Section that moves the camera based on where the current camera is pointing
        Vector3 forwardRelativeVerticalInput = movementY * forward;
        Vector3 rightRelativeVerticalInput = movementX * right;
        Vector3 move = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        float magnitude = Mathf.Clamp01(move.magnitude) * speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        //Jump section
        if(characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffSet;
            ySpeed = -0.5f;
            if(jumpPress)
            {
                ySpeed = jumpSpeed;
                jumpPress = false;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = move * magnitude;
        velocity.y = ySpeed;

        //Rotates Player model to move direction
        if (move != Vector3.zero)
        {
            Quaternion playerRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        }

        //Moves the player
        characterController.Move(velocity * Time.deltaTime);
    }
}
