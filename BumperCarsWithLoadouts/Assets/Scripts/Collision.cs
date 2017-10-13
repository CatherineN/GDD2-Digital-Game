using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float impactForce;

    private Rigidbody rb;
    private Player p;

    private int collisionCount;
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        p = gameObject.GetComponent<Player>();
        collisionCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        collisionCount = 0;
	}

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" ||collisionCount > 0)
            return;

        Vector3 between = Vector3.Normalize(collision.transform.position - transform.position);
        float vProj = Vector3.Dot(rb.velocity, between);

        Vector3 force = vProj * rb.velocity * impactForce;

        rb.AddForce(-force);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
        //collision.transform.forward = Vector3.Lerp(collision.transform.forward, collision.transform.forward + force, Time.deltaTime);
        collision.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Vector3.Cross(collision.gameObject.transform.forward, force).sqrMagnitude * 100.0f, 0), ForceMode.Impulse);

        Debug.Log("Collision");

        collisionCount++;
    }
}
