using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {

    [SerializeField]
    private Transform followPoint = default;

    [SerializeField]
    private float moveSpeed = 1;

    [SerializeField]
    private float rotateSpeed = 1;

    public static Camera Camera { get; private set; }

    private void Awake() {
        Camera = GetComponentInChildren<Camera>();
    }

    private void Start() {
        transform.position = followPoint.position;
        transform.rotation = followPoint.rotation;
    }

    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, followPoint.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, followPoint.rotation, rotateSpeed * Time.deltaTime);
    }

}