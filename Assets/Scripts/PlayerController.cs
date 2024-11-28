using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10;
    private CharacterController controller;
    private float jump = 15;
    private float gravity = 5;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            moveDirection.y = jump;
        }

        moveDirection.y += (Physics.gravity.y * gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
