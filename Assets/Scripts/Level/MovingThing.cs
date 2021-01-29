using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThing : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    private Transform offPosition, onPosition;
    private Rigidbody thisRigidbody;

    private int numberOfButtonsTurningOn = 0;

    private void Awake() {
        offPosition = transform.Find("Off Position");
        onPosition = transform.Find("On Position");
        thisRigidbody = GetComponentInChildren<Rigidbody>();
    }

    public void TurnOn() {
        numberOfButtonsTurningOn++;
    }

    public void TurnOff() {
        numberOfButtonsTurningOn--;
    }

    private void FixedUpdate() {
        Vector3 targetPosition = numberOfButtonsTurningOn > 0 ? onPosition.position : offPosition.position;
        thisRigidbody.MovePosition(Vector3.MoveTowards(thisRigidbody.position, targetPosition, moveSpeed * Time.deltaTime));//no need for deltaTime since this is FixedUpdate, but in anyway to make the unit seconds
    }

}