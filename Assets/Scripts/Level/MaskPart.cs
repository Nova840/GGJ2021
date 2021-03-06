using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskPart : MonoBehaviour {

    [SerializeField]
    private string partName = "";

    private Renderer[] allChildRenderers;

    private static List<MaskPart> allMaskPartsInScene = new List<MaskPart>();

    private bool pickedUp = false;

    private void Awake() {
        allChildRenderers = GetComponentsInChildren<Renderer>();
        allMaskPartsInScene.Add(this);
    }

    private void OnDestroy() {
        allMaskPartsInScene.Remove(this);
    }

    private void Start() {
        bool alreadyPickedUp = PlayerPrefs.GetInt(GetUniqueName(), 0) == 1;
        if (alreadyPickedUp) {
            AddMaskPartAndMakeInvisible();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerPrefs.SetInt(GetUniqueName(), 1);
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " % Complete", (float)NumberOfPickedUpMaskPartsInScene() / NumberOfMaskPartsInScene());
            AddMaskPartAndMakeInvisible();
        }
    }

    private void AddMaskPartAndMakeInvisible() {
        MaskUI.Refresh();
        pickedUp = true;
        foreach (Renderer r in allChildRenderers) {
            r.enabled = false;
        }
    }

    private string GetUniqueName() {
        return SceneManager.GetActiveScene().name + ":" + partName;
    }

    public static int NumberOfMaskPartsInScene() {
        return allMaskPartsInScene.Count;
    }

    public static int NumberOfPickedUpMaskPartsInScene() {
        return allMaskPartsInScene.Count(mp => mp.pickedUp);
    }

}