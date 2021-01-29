using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float acceleration = .25f;

    [SerializeField]
    private float gravity = 10;

    private CharacterController characterController;

    private float currentYVelocity = 0;

    private Vector3 smoothMoveVectorXZ = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Vector3 moveVectorXZ = new Vector3(inputVector.x, 0, inputVector.y);
        moveVectorXZ *= moveSpeed * Time.deltaTime;
        if (characterController.isGrounded) {
            currentYVelocity = 0;
        } else {
            currentYVelocity -= gravity * Time.deltaTime;//multiplied by delta time twice on purpose
        }

        smoothMoveVectorXZ = Vector3.MoveTowards(smoothMoveVectorXZ, moveVectorXZ, acceleration * Time.deltaTime);
        Vector3 move = new Vector3(smoothMoveVectorXZ.x, currentYVelocity, smoothMoveVectorXZ.z);
        characterController.Move(move);
    }

}