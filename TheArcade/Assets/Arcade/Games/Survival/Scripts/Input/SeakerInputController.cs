using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeakerInputController : InputController
{
    public string targetTag;

	// Use this for initialization
	void Start ()
	{
	    var target = GameObject.FindGameObjectWithTag(targetTag);
	    var vector = target.transform.position - this.transform.position;
	    vector.y = 0;
	    vector.Normalize();
	    Horizontal = vector.x;
	    Vertical = vector.z;
	}
}
