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
                    Block block = hit.collider.GetComponentInParent<Block>();
                    if (!IsStandingAboveBlock(block)) {
                        PickUpBlock(block);
                    }
                } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("NPC")) {
                    hit.collider.GetComponentInParent<NPC>().Interact();
                }
            }
        }
        if (Input.GetButtonUp("Fire1") || IsStandingAboveBlock(currentMovingBlock)) {
            DropCurrentBlock();
        }
    }

    private void PickUpBlock(Block block) {
        DropCurrentBlock();
        currentMovingBlock = block;
        currentMovingBlock.FollowMousePosition = true;
    }

    public void DropCurrentBlock() {
        if (currentMovingBlock) {
            currentMovingBlock.FollowMousePosition = false;
        }
        currentMovingBlock = null;
    }

    private bool IsStandingAboveBlock(Block block) {
        if (!block) return false;
        Vector3 playerPosXZ = transform.position;
        playerPosXZ.y = block.transform.position.y;
        float xzDist = Vector3.Distance(playerPosXZ, block.transform.position);
        float yDist = transform.position.y - block.transform.position.y;
        return xzDist <= 2 && yDist >= 0 && yDist <= 5;//block/jump size
    }

}