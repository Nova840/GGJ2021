using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachAndDontDestroyOnLoad : MonoBehaviour {

    private static List<string> started = new List<string>();

    private void Start() {
        if (started.Contains(name)) {
            Destroy(gameObject);
            return;
        }
        started.Add(name);
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

}