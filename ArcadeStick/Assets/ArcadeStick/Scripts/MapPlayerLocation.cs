using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerLocation : MonoBehaviour
{

    public GameObject markerPrefab;

    private GameObject instantiatedMarker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    InstantiateMarker(hit);
                }
            }
        }
    }

    private void InstantiateMarker(RaycastHit hit)
    {
        if (instantiatedMarker != null)
        {
            Destroy(instantiatedMarker);
        }

        var instantiated = Instantiate(markerPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        instantiated.transform.parent = this.transform;

        instantiatedMarker = instantiated;
    }
}
