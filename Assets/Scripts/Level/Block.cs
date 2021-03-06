using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField]
    private float dragSpeed = 1;

    public enum BlockType {
        Red, Yellow, Green
    }

    [SerializeField]
    private BlockType blockType = BlockType.Red;
    public BlockType Type { get => blockType; }

    private Rigidbody thisRigidbody;

    private bool followMousePosition = false;
    public bool FollowMousePosition {
        get { return followMousePosition; }
        set {
            followMousePosition = value;
            if (followMousePosition) {
                followPositionY = thisRigidbody.position.y;
            } else {
                thisRigidbody.velocity = Vector3.zero;
            }
        }
    }

    private float followPositionY = 0;


    private Quaternion startingRotation;

    private void Awake() {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        startingRotation = transform.rotation;
    }

    //https://gamedev.stackexchange.com/a/75662
    private static bool GetMousePositionOnXZPlane(float planeHeight, out Vector3 pos) {
        Plane XZPlane = new Plane(Vector3.up, Vector3.up * planeHeight);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = XZPlane.Raycast(ray, out distance);
        if (didHit) {
            Vector3 hitPoint = ray.GetPoint(distance);
            //Just double check to ensure the y position is exactly planeHeight
            hitPoint.y = planeHeight;
            pos = hitPoint;
        } else {
            pos = Vector3.zero;
        }
        return didHit;
    }

    private void FixedUpdate() {
        if (FollowMousePosition) {
            bool didHit = GetMousePositionOnXZPlane(followPositionY, out Vector3 point);
            if (didHit) {
                Vector3 currentTargetPosition = Vector3.Lerp(thisRigidbody.position, point, dragSpeed * Time.deltaTime);
                thisRigidbody.velocity = (currentTargetPosition - thisRigidbody.position) / Time.fixedDeltaTime;

                Vector3 pos = thisRigidbody.position;
                pos.y = followPositionY;
                thisRigidbody.MovePosition(pos);
            } else {
                FollowMousePosition = false;
            }
        }
        thisRigidbody.angularVelocity = Vector3.zero;
    }

    private void LateUpdate() {
        transform.rotation = startingRotation;
    }

}