using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlanetGrab : MonoBehaviour
{
    public float grabStrength;

    private Rigidbody rigidbody;
    private bool grabbed;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetMouseButtonDown(1))
	    {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit))
	        {
	            if (hit.transform.gameObject == this.gameObject)
	            {
	                grabbed = true;
	            }
	        }
	    }

	    if (grabbed && Input.GetMouseButton(1))
        {
            rigidbody.AddTorque(-Camera.main.transform.up * Input.GetAxis("Mouse X") * grabStrength);
            rigidbody.AddTorque(Camera.main.transform.right * Input.GetAxis("Mouse Y") * grabStrength);
        }
	    else
	    {
	        grabbed = false;
	    }
	}
}
