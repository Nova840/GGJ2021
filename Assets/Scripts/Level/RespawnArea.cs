using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour {

    private Transform spawnpoint;

    private void Awake() {
        spawnpoint = transform.Find("Spawnpoint");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CharacterController characterController = other.gameObject.GetComponentInParent<CharacterController>();
            characterController.enabled = false;
            characterController.transform.position = spawnpoint.position;
            characterController.transform.rotation = spawnpoint.rotation;
            characterController.enabled = true;
        }
    }

}