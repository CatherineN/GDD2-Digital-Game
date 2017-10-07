using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// READ THE HHHfUCKING COMMENTS.
//
public class VehicleMovement : MonoBehaviour {

    // **For this, we are assuming that the mass of the bumper car will be 1**
    // variables
    Vector3 position;
    Vector3 direction;
    Vector3 velocity;
    Vector3 desiredVelocity;
    Vector3 acceleration;

    float maxSpeed = 1;
    float minSpeed = 4;
    float turnSpeed = 0.9f;
    float totalRotation = 0; // add or subtract 1 when rotating the bumper car
    float damping = 5; // this will slow down the bumper car as we turn

    // Use this for initialization
    void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        CalcSteeringForces();
        UpdatePosition();
        SetTransform();
    }

    /// <summary>
    /// Takes player input and calculates the forces acting on it accordingly
    /// Needs a max force**
    /// </summary>
    protected void CalcSteeringForces()
    {
        // this is for our rotation
        Quaternion angleToRotate = Quaternion.Euler(0, 0, 0);

        // this is the sum of all the forces
        Vector3 total = Vector3.zero;

        Debug.Log("  Y: " + Input.GetAxis("Vertical") + "  X: " + Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0) // go forward
        {
            direction = transform.forward;
            total += direction;
            
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0) // turns counter-clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
            totalRotation -= turnSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0) // turns clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
            totalRotation += turnSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0) // go backward
        {
            direction = -transform.forward;
            total += direction;
        }

        // apply the sum of the forces to our bumper car
        ApplyForce(total);
        direction = angleToRotate * direction;

    }

    /// <summary>
    /// UpdatePosition
    /// Calculate the velocity and resulting position of a vehicle
    /// based on any forces
    /// </summary>
    protected void UpdatePosition()
    {
        position = transform.position;
        //add acceleration to velocity
        velocity += acceleration * Time.deltaTime;
        //clamp velocity
        Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity.y = 0;
        //add velocity to position
        position += velocity;
        position.y = 0;
        //calculate direction from velocity
        direction = velocity.normalized;
        //zero out acceleration
        acceleration = Vector3.zero;
    }


    /// <summary>
    /// Sets the transform component to the local positon
    /// </summary>
    protected void SetTransform()
    {
        //set up vector equal to the seekers direction
        transform.forward = direction;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, totalRotation, 0);
    }


    /// <summary>
    /// Applies any Vector3 force to the acceleration vector
    /// </summary>
    /// <param name="force">Force.</param>
    protected void ApplyForce(Vector3 force)
    {
        acceleration += force;
    }

}
