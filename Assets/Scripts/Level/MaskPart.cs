using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPart : MonoBehaviour {

    private Renderer[] allChildRenderers;//can't just destroy the object because the coroutine is running

    private void Awake() {
        allChildRenderers = GetComponentsInChildren<Renderer>();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            foreach (Renderer r in allChildRenderers) {
                r.enabled = false;
            }
            CompleteLevel.Complete();
        }
    }

}