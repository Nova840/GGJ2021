using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour {

    [Serializable]
    private class UnityEventCollider : UnityEvent<Collider> { }

    [SerializeField]
    private UnityEventCollider onTriggerEnter = new UnityEventCollider();

    [SerializeField]
    private UnityEventCollider onTriggerExit = new UnityEventCollider();

    [SerializeField]
    private UnityEventCollider onTriggerStay = new UnityEventCollider();

    private void OnTriggerEnter(Collider other) {
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other) {
        onTriggerExit.Invoke(other);
    }

    private void OnTriggerStay(Collider other) {
        onTriggerStay.Invoke(other);
    }

}