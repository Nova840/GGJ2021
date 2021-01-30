using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [SerializeField]
    private Vector3 rotateAmount = new Vector3(0, 5, 0);

    [SerializeField]
    private Space relativeTo = Space.Self;

    private void Update() {
        transform.Rotate(rotateAmount * Time.deltaTime, relativeTo);
    }

}