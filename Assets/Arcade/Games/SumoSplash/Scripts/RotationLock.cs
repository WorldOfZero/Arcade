using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotationLock : MonoBehaviour {

    private Rigidbody rigidbody;
    public float strength;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetRotation = Vector3.forward;
        var rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(targetRotation), Time.deltaTime * strength);
        rigidbody.MoveRotation(rotation);

        //rigidbody.AddTorque(Vector3.up * Vector3.Dot(targetRotation, this.transform.right) * Time.deltaTime * strength, ForceMode.Acceleration);
	}
}
