using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [SerializeField]
    private Vector3 rotateAmount = Vector3.zero;

    [SerializeField]
    private Space relativeTo = Space.World;

    private void Update() {
        transform.Rotate(rotateAmount * Time.deltaTime, relativeTo);
    }

}