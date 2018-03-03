using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    //variables for speed and velocity
    private float speed;
    public Vector3 velocity;
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
        transform.forward = direction;
        transform.position += velocity;
        if (timer == 100)
            Destroy(gameObject);
        timer++;

        // check for a collision
        RaycastHit hit;
        if (Physics.Raycast(transform.position, velocity, out hit, speed))
        {
            if (hit.collider.tag == "Player" || hit.collider.tag == "AI")
            {
                hit.collider.gameObject.GetComponent<Collision>().ProjectileHit(this.GetComponent<SphereCollider>());
                transform.position = hit.point;
            }
        }
	}
}
