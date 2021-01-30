using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnOnStart : MonoBehaviour {

    private void Start() {
        Portal.TeleportToLastAssignedPortal(GetComponent<CharacterController>());
    }

}