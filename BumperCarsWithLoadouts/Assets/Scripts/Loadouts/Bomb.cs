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
    //array to collect every collider inside the radius


    void Start ()
    {
        force = 20f;
        radius = 2f;
        timer = 300f;
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
                //If they do, add an explosion force. Thiiiiiiis doesn't quite work yet. Need to tweak...something.
                c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 120f, ForceMode.Impulse);
            }
            //Get rid of it
            Destroy(gameObject);
        }
	}
}
