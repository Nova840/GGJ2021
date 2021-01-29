using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public static Transform Point { get; private set; }

    private void Awake() {
        Point = transform;
    }

}