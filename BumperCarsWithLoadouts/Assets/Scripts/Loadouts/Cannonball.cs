using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    //variables for speed and velocity
    private float speed;
    private Vector3 velocity;
    public Vector3 direction;
    int timer;
	// Use this for initialization
	void Start ()
    {
        speed = 2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity = direction * speed;
        transform.position += velocity;
        if (timer == 100)
            Destroy(gameObject);
        timer++;
	}
}
