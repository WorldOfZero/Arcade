using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    private InputHandler input;
    private Rigidbody rigidbody;

    public float speed = 10;

	// Use this for initialization
	void Start () {
        input = GetComponent<InputHandler>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rigidbody.AddForce(Vector3.forward * input.Vertical * speed);
        rigidbody.AddForce(Vector3.right * input.Horizontal * speed);
    }
}
