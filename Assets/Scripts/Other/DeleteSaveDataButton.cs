using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSaveDataButton : MonoBehaviour {

    public void DeleteAllSaveData() {
        PlayerPrefs.DeleteAll();
    }

}