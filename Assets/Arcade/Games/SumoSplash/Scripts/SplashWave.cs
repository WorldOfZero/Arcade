using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplashWave : MonoBehaviour {

    public float radius;
    public float edgeSize;

    public float speed;
    public float amplitude;

    public float resistance;

    public float waveHeight;

    public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (amplitude < 0)
        {
            Destroy(this.gameObject);
        }

        amplitude -= resistance * Time.deltaTime;
        radius += speed * Time.deltaTime;

        var outerSet = Physics.OverlapSphere(this.transform.position, radius);
        var innerSet = Physics.OverlapSphere(this.transform.position, radius - edgeSize);
        foreach (var physicsObject in outerSet.Except(innerSet))
        {
            if (physicsObject.transform.position.y > this.transform.position.y + waveHeight)
            {
                continue;
            }
            if (owner != physicsObject.gameObject)
            {
                var rigidbody = physicsObject.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddForce((rigidbody.position - this.transform.position).normalized * amplitude * Time.deltaTime);
                }
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        DrawCircle(this.transform.position, radius);
        Gizmos.color = Color.blue;
        DrawCircle(this.transform.position, radius - edgeSize);
    }

    private void DrawCircle(Vector3 position, float radius, int precision = 32)
    {
        Vector3 previousPoint = position + this.transform.right * radius;
        for (int i = 1; i <= precision; ++i)
        {
            var angle = 2 * Mathf.PI * (i / (float)precision);
            Vector3 newPoint = position
                + this.transform.right * radius * Mathf.Cos(angle)
                + this.transform.forward * radius * Mathf.Sin(angle);
            Gizmos.DrawLine(newPoint, previousPoint);
            previousPoint = newPoint;
        }
    }
}
