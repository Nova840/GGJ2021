using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    [SerializeField]
    private LayerMask layerMask = ~0;

    private Block currentMovingBlock = null;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = CameraRig.Camera.ScreenPointToRay(Input.mousePosition);
            bool didHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask);
            if (didHit) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Block")) {
                    currentMovingBlock = hit.collider.GetComponentInParent<Block>();
                    currentMovingBlock.FollowMousePosition = true;
                } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("NPC")) {
                    hit.collider.GetComponentInParent<NPC>().Interact();
                }
            }
        }
        if (Input.GetButtonUp("Fire1")) {
            if (currentMovingBlock) {
                currentMovingBlock.FollowMousePosition = false;
            }
            currentMovingBlock = null;
        }
    }

}