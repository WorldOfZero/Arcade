using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputHandler))]
public class PlayerJumpController : MonoBehaviour {

    public AnimationCurve jumpHeight;
    public GameObject attackObject;
    private InputHandler input;

    private float animationTime;
    private bool grounded;
    private bool isJumping;

    private float jumpTimeframe;

	// Use this for initialization
	void Start () {
        isJumping = false;
        input = GetComponent<InputHandler>();
        grounded = true;
        jumpTimeframe = jumpHeight.keys[jumpHeight.length - 1].time;
	}
	
	// Update is called once per frame
	void Update () {
        if (isJumping)
        {
            this.transform.position = new Vector3(this.transform.position.x, jumpHeight.Evaluate(animationTime), this.transform.position.z);
            animationTime += Time.deltaTime;
            if (animationTime > jumpTimeframe)
            {
                animationTime = 0;
                isJumping = false;

                var spawned = Instantiate(attackObject, this.transform.position, Quaternion.identity);
                var wave = spawned.GetComponent<SplashWave>();
                if (wave != null)
                {
                    wave.owner = this.gameObject;
                }
            }
        }
        else if (input.Jump && grounded)
        {
            isJumping = true;
        }
	}
}
