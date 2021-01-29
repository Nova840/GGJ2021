using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour {

    [SerializeField]
    private Vector3 eulerAngles = new Vector3(60, 0, 0);

    private void LateUpdate() {
        transform.eulerAngles = eulerAngles;
    }

}