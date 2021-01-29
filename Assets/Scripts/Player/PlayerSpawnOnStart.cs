using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnOnStart : MonoBehaviour {

    private CharacterController characterController;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Start() {
        characterController.enabled = false;
        transform.position = Spawnpoint.Point.position;
        transform.rotation = Spawnpoint.Point.rotation;
        characterController.enabled = true;
    }

}