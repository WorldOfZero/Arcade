using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingInputController : InputController
{

    public float horizontalBound;
    public float verticalBound;

	// Use this for initialization
	void Start ()
	{
	    var random = Random.insideUnitCircle.normalized;
	    Horizontal = random.x;
	    Vertical = random.y;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Horizontal > 0 && transform.position.x > horizontalBound)
	    {
	        Horizontal *= -1;
	    }
	    else if (Horizontal < 0 && transform.position.x < -horizontalBound)
	    {
	        Horizontal *= -1;
	    }

	    if (Vertical > 0 && transform.position.z > verticalBound)
	    {
	        Vertical *= -1;
	    }
	    else if (Vertical < 0 && transform.position.z < -verticalBound)
	    {
	        Vertical *= -1;
	    }
    }
}
