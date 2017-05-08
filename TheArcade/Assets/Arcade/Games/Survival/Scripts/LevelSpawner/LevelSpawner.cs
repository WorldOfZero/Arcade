using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject spawn;
    public float spawnTimer;

    private float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime;

	    if (timer > spawnTimer)
	    {
	        timer = 0;
	        Tick();
	    }
	}

    void Tick()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(spawn, spawnPoint.position, Quaternion.identity);
    }
}
