using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPunch : MonoBehaviour {

    // Use this for initialization
    //variables for speed and velocity
    private float speed;
    public Vector3 velocity;
    public Vector3 direction;
    int timer;
    void Start ()
    {
        speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity = direction * speed;
        transform.forward = direction;
        transform.position += velocity;
       // if (timer == 100)
           // Destroy(gameObject);
       // timer++;
    }
}
