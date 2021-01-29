using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour {

    [SerializeField]
    private float loadStartDelay = 3;

    private static CompleteLevel instance;

    private static bool completed = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        completed = false;
    }

    public static void Complete() {
        if (completed) return;
        completed = true;
        instance.gameObject.SetActive(true);
        instance.StartCoroutine(instance.LoadStartAfterDelay());
    }

    private IEnumerator LoadStartAfterDelay() {
        yield return new WaitForSeconds(loadStartDelay);
        SceneManager.LoadScene("Start");
    }

}