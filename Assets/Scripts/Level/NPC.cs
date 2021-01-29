using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    [SerializeField, TextArea(5, 5)]
    private string[] npcText = new string[0];

    public void Interact() {
        if (!DialogManager.HasText()) {
            DialogManager.AddToTypeQueue(npcText);
        }
    }

}