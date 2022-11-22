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
        Vector3 move = new Vector3(movementX, 0, movementY);
        float magnitude = Mathf.Clamp01(move.magnitude) * speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

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


        Vector3 velocity = move;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
    }

}
