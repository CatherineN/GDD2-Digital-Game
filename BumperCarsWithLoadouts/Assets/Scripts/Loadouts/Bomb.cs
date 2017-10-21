using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    // Use this for initialization
    //The force and the radius of the explosion
    public float force;
    public float radius;
    //How long it will take for the bomb to explode
    public float timer;
    //force vector
    private Vector3 explosionForce;
    private float magnitude;


    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Decrement the timer
        timer--;
        //When it reaches 0...
        if (timer == 0)
        {
            //Get every collider near it
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            //And for all of them...
            foreach (Collider c in colliders)
            {
                //If they don't have a rigidbody, ignore them
                if (c.GetComponent<Rigidbody>() == null)
                    continue;
                //Get the square magnitude
                magnitude = (transform.position - c.transform.position).sqrMagnitude;
                //Generate the explosion force
                explosionForce = (c.transform.position - transform.position).normalized * (1 / magnitude) * force;
                //Make sure they dont FLY TO THE MOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOON
                explosionForce.y = 0;
                //Apply the explosion force
                c.GetComponent<VehicleMovement>().ApplyForce(explosionForce);
            }
            //Get rid of it
            Destroy(gameObject);
        }
	}
}
