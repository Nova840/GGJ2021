using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private List<GameObject> directChildren = new List<GameObject>();

    private bool paused = false;

    private void Awake() {
        foreach (Transform child in transform) {
            directChildren.Add(child.gameObject);
        }
    }

    private void Start() {
        SetPaused(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            SetPaused(!paused);
        }
    }

    private void SetPaused(bool paused) {
        this.paused = paused;
        foreach (GameObject child in directChildren) {
            child.SetActive(paused);
        }
    }

    public void DeleteAllSaveData() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Hub");
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}