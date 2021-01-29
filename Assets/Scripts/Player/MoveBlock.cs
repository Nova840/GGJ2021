using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {

    [SerializeField]
    private LayerMask blockLayerMask = ~0;

    private Block currentMovingBlock = null;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = CameraRig.Camera.ScreenPointToRay(Input.mousePosition);
            bool didHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, blockLayerMask);
            if (didHit) {
                currentMovingBlock = hit.transform.GetComponent<Block>();
                currentMovingBlock.FollowMousePosition = true;
            }
        }
        if (Input.GetButtonUp("Fire1")) {
            if (currentMovingBlock) {//can be null if you start the game while holding the button down
                currentMovingBlock.FollowMousePosition = false;
            }
            currentMovingBlock = null;
        }
    }

}