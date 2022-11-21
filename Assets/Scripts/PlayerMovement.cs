using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    public float speed = 5f;
    public float gravity = -1.0f;
    private float movementX;
    private float movementY;
    Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void OnMove(InputValue value)
    {
       Vector2 move = value.Get<Vector2>();

       movementX = move.x;
       movementY = move.y;
    }
    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Vector3 move = new Vector3(movementX, 0, movementY);
        characterController.Move(move * Time.deltaTime * speed);
    }
}
