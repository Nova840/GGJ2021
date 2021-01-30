using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThing : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    private Vector3 offPosition;
    private Transform  onPositionTransform;
    private Rigidbody thisRigidbody;

    private int numberOfButtonsTurningOn = 0;

    private void Awake() {
        onPositionTransform = transform.Find("On Position");
        thisRigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Start() {
        offPosition = thisRigidbody.transform.position;
    }

    public void TurnOn() {
        numberOfButtonsTurningOn++;
    }

    public void TurnOff() {
        numberOfButtonsTurningOn--;
    }

    private void FixedUpdate() {
        Vector3 targetPosition = numberOfButtonsTurningOn > 0 ? onPositionTransform.position : offPosition;
        thisRigidbody.MovePosition(Vector3.MoveTowards(thisRigidbody.position, targetPosition, moveSpeed * Time.deltaTime));//no need for deltaTime since this is FixedUpdate, but in anyway to make the unit seconds
    }

}