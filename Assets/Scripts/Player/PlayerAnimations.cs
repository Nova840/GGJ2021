using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator animator;
    private PlayerMove playerMove;
    private CharacterController characterController;

    private float timeStarted;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        playerMove = GetComponent<PlayerMove>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start() {
        timeStarted = Time.time;
    }

    private void Update() {
        animator.SetFloat("Movement", characterController.velocity.magnitude / playerMove.MoveSpeed);
        bool falling = !characterController.isGrounded;
        if (Time.time - timeStarted < .25f) {//should start in the scene as not falling
            falling = false;
        }
        animator.SetBool("IsFalling", falling);
    }

}