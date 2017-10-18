using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float impactForce;
    public float torqueAngle;
    public float timeStep;
    public float carLength;

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Terrain" || other.gameObject.tag == "Projectile" || collisionCount > 0)
            return;

        #region Obsolete Code
        /*Vector3 between = Vector3.Normalize(collision.transform.position - transform.position);
        float vProj = Vector3.Dot(p.Velocity, between);

        Vector3 force = vProj * p.Velocity * impactForce;

        //p.ApplyForce(-force * (collision.gameObject.GetComponent<Rigidbody>().mass / rb.mass));
        //collision.gameObject.GetComponent<VehicleMovement>().ApplyForce(force * (rb.mass / collision.gameObject.GetComponent<Rigidbody>().mass));
        rb.AddForceAtPosition(-force, transform.position, ForceMode.Impulse);
        collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, collision.transform.position, ForceMode.Impulse);
        //collision.transform.forward = Vector3.Lerp(collision.transform.forward, collision.transform.forward + force, Time.deltaTime);
        //collision.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Vector3.Cross(collision.gameObject.transform.forward, force).sqrMagnitude * 100.0f, 0), ForceMode.Impulse);

        // Calculate the "torque" force
        /*float right = 0.0f;
        if (Vector3.Dot(force, collision.transform.forward) > 0)
            right = 1.0f;
        else
            right = -1.0f;

        float angle = torqueAngle * force.sqrMagnitude * right;
        Quaternion goal = Quaternion.Euler(collision.transform.rotation.eulerAngles.x, collision.transform.rotation.eulerAngles.y + angle, collision.transform.rotation.eulerAngles.z);

        // stop any current torque calculations
        StartCoroutine(ApplyTorque(goal, Mathf.Abs(angle) * timeStep, collision.gameObject));
        Debug.Log("Force: " + force.sqrMagnitude);*/
        #endregion

        ResetCar(other);

        Debug.Log("Trigger");

        collisionCount++;
    }



    IEnumerator ApplyTorque(Quaternion toRotate, float time, GameObject target)
    {
        while(target.transform.rotation.eulerAngles.y < toRotate.eulerAngles.y - 1.0f || target.transform.rotation.eulerAngles.y > toRotate.eulerAngles.y + 1.0f)
        {
            target.GetComponent<Player>().TotalRotation = Mathf.LerpAngle(target.GetComponent<Player>().TotalRotation, toRotate.eulerAngles.y, time);
            yield return null;
        }
    }

    public void ResetCar(Collider other)
    {
        /*RaycastHit hit;
        if(!other.Raycast(new Ray(transform.position, transform.forward), out hit, 100.0f))
        {
            other.Raycast(new Ray(transform.position, -transform.forward), out hit, 100.0f);
        }
        transform.position = hit.point;*/
        transform.position += -(p.Velocity);
    }
}
