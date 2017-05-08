using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaggingBasicInputController : InputController
{

    public float delay;
    private Queue<MovementBuffer> movementBuffer = new Queue<MovementBuffer>();

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        var buffer = new MovementBuffer()
        {
            time = Time.time,
            horizontal = Input.GetAxis("Horizontal"),
            vertical = Input.GetAxis("Vertical")
        };
        movementBuffer.Enqueue(buffer);

        if (movementBuffer.Count > 0)
        {
            if (movementBuffer.Peek().time + delay < Time.time)
            {
                var movement = movementBuffer.Dequeue();
                Horizontal = movement.horizontal;
                Vertical = movement.vertical;
            }
        }
    }

    private struct MovementBuffer
    {
        public float time;
        public float horizontal;
        public float vertical;
    }
}
