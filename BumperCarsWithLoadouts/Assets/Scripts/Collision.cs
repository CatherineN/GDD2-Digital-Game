using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float impactForce;

    private VehicleMovement vm;
	// Use this for initialization
	void Start ()
    {
        vm = gameObject.GetComponent<VehicleMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
            return;

        Vector3 between = Vector3.Normalize(collision.transform.position - transform.position);
        float vProj = Vector3.Dot(vm.Velocity, between);

        Vector3 force = (vProj * between) * impactForce;

        vm.ApplyForce(-force);
        collision.gameObject.GetComponent<VehicleMovement>().ApplyForce(force);
        collision.transform.forward = Vector3.Lerp(collision.transform.forward, collision.transform.forward + force, Time.deltaTime);

        Debug.Log("Collision with force " + force);
    }
}
