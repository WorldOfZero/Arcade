using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInputController : InputController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    Horizontal = Input.GetAxis("Horizontal");
	    Vertical = Input.GetAxis("Vertical");
    }
}
