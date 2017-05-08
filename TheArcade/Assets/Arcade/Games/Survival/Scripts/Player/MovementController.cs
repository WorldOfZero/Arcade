using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Vector3 movement;

    public InputController input;
    public ShapeController shape;
	
	// Update is called once per frame
	void Update ()
	{
	    movement.x = input.Horizontal;
	    movement.z = input.Vertical;
        this.transform.position += movement * shape.speed * Time.deltaTime;
	}
}
