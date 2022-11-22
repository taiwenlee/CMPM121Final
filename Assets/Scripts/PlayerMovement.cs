using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;

    public float speed = 5f;
    public float jumpSpeed;
    public float rotationSpeed;

    private float ySpeed;
    private float movementX;
    private float movementY;
    private float originalStepOffSet;
    private bool jumpPress = false;

    private void Start()
    {
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
    private void Update()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
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
