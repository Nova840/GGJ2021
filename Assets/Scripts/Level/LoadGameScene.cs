using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "Game";

    private void Awake() {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
    }

}