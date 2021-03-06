using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 10;
    public float MoveSpeed { get => moveSpeed; }

    [SerializeField]
    private float acceleration = 150;

    [SerializeField]
    private float gravity = 10;

    [SerializeField]
    private float jumpHeight = 1;

    private CharacterController characterController;

    private float currentYVelocity = 0;

    private Vector3 smoothMoveVectorXZ = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
        RespawnIfFallenOff();
    }

    private void Move() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector = Vector2.ClampMagnitude(inputVector, 1);


        Vector3 moveVectorXZ = new Vector3(inputVector.x, 0, inputVector.y);
        moveVectorXZ *= moveSpeed;
        if (characterController.isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                currentYVelocity = Mathf.Sqrt(-jumpHeight * -2 * gravity);//formula to calculate velocity from jump height
            } else {
                currentYVelocity = -2;
            }
        } else {
            currentYVelocity -= gravity * Time.deltaTime;//multiplied by delta time twice on purpose
        }

        smoothMoveVectorXZ = Vector3.MoveTowards(smoothMoveVectorXZ, moveVectorXZ, acceleration * Time.deltaTime);
        Vector3 move = new Vector3(smoothMoveVectorXZ.x, currentYVelocity, smoothMoveVectorXZ.z);
        characterController.Move(move * Time.deltaTime);

        if (smoothMoveVectorXZ != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(smoothMoveVectorXZ, Vector3.up);
        }
    }

    private void RespawnIfFallenOff() {
        if (transform.position.y <= -100) {
            Portal.TeleportToLastAssignedPortal(characterController);
        }
    }

}