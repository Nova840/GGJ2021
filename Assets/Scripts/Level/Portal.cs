using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    [SerializeField]
    private string portalName = "";

    [SerializeField]
    private string destinationSceneName = "";

    [SerializeField]
    private string destinationPortalName = "";

    private static string lastDestinationPortalName = "start";//teleport to starting portal

    private static Dictionary<string, Transform> spawnpointsForPortals = new Dictionary<string, Transform>();

    private void Awake() {
        if (spawnpointsForPortals.ContainsKey(portalName)) {
            Debug.LogError("Two or more portals have the same name.");
        }
        spawnpointsForPortals.Add(portalName, transform.Find("Spawnpoint"));
    }

    private void OnDestroy() {
        if (spawnpointsForPortals.ContainsKey(portalName)) {
            spawnpointsForPortals.Remove(portalName);
        }
    }

    private static Transform GetSpawnpointForPortal(string pName) {
        if (!spawnpointsForPortals.ContainsKey(pName)) {
            Debug.Log("Portal \"" + pName + "\" does not exist in this scene, picking the first portal.");
            return spawnpointsForPortals.First().Value;
        }
        return spawnpointsForPortals[pName];
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            lastDestinationPortalName = destinationPortalName;
            if (SceneManager.GetActiveScene().name != destinationSceneName) {
                SceneManager.LoadScene(destinationSceneName);
            } else {
                TeleportToLastAssignedPortal(other.GetComponentInParent<CharacterController>());
            }
        }
    }

    public static void TeleportToLastAssignedPortal(CharacterController characterController) {
        characterController.enabled = false;
        Transform spawnpoint = GetSpawnpointForPortal(lastDestinationPortalName);
        characterController.transform.position = spawnpoint.position;
        characterController.transform.rotation = spawnpoint.rotation;
        characterController.enabled = true;
        characterController.GetComponent<PlayerInteract>().DropCurrentBlock();//let go of block when teleporting
    }

}