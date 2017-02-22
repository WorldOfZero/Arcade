using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class LocalController : MonoBehaviour {

    private InputHandler input;

	// Use this for initialization
	void Start () {
        input = GetComponent<InputHandler>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        input.Horizontal = Input.GetAxis("Horizontal");
        input.Vertical = Input.GetAxis("Vertical");
        input.Jump = Input.GetButton("Jump");
    }
}
