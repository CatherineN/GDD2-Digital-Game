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
    Vector3 total;//total force acting on the gameobject every frame
    Quaternion angleToRotate;//how far to rotate this frame

    public int playerID;//which player the script is on

    public float maxForce;
    float maxSpeed = .75f;
    float minSpeed = .4f;
    float turnSpeed = 1.9f;
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
        angleToRotate = Quaternion.Euler(0, 0, 0);

        // this is the sum of all the forces
        total = Vector3.zero;

        switch (playerID)
        {
            case 1:
                GetInputPlayer1();
                break;
            case 2:
                GetInputPlayer2();
                break;
        }

        //clamp the total force to maxForce
        total = Vector3.ClampMagnitude(total, maxForce);
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(angleToRotate * direction, transform.forward, Time.deltaTime * 0.5f);
        ApplyFriction(CalculateCoefficientFriction(0.5f, 2.0f));

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
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        if(velocity.magnitude < .01f)
        {
            velocity = Vector3.zero;
        }
        velocity.y = 0;
        //add velocity to position
        position += velocity;
        //position.y = 0;
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

    /// <summary>
	/// Applies the friction.
	/// </summary>
	/// <param name="coeff">Coeff. of friction</param>
	public void ApplyFriction(float coeff)
    {
        //step 1: get the negation of the velocity
        Vector3 friction = velocity * -1;
        //step 2: get the normalized vector so it is independent of vel mag
        friction.Normalize();
        //step 3: multiply by coeff
        friction = friction * coeff;
        //step 4: apply to acceleration
        acceleration += friction;
    }

    /// <summary>
    /// Uses a rejection to more realistically apply friction.
    /// Cars sliding sideways have more friction than moving straight
    /// </summary>
    /// <param name="coeff">Standard coefficient of friction</param>
    /// <param name="force">How tight or slidey the car is. Lower number = slidey, Higher number = tight</param>
    /// <returns>The coefficient of friction to use</returns>
    public float CalculateCoefficientFriction(float coeff, float force)
    {
        // find the projection of the forward onto velocity
        Vector3 projection = Vector3.Dot(transform.forward, velocity.normalized) * velocity.normalized;

        // find the rejection (perpendicular vector from velocity to direction)
        Vector3 rejetion = transform.forward - projection;

        // take the magnitude squared and combine it with the standard friction
        return (rejetion.sqrMagnitude * force) + coeff;
    }

<<<<<<< HEAD
}
=======

    void GetInputPlayer1()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical_P1") > 0) // go forward
        {
            direction = transform.forward;
            total += direction;

        }
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal_P1") < 0) // turns counter-clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
            totalRotation -= turnSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal_P1") > 0) // turns clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
            totalRotation += turnSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical_P1") < 0) // go backward
        {
            direction = -transform.forward;
            total += direction;
        }

    }

    void GetInputPlayer2()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical_P2") > 0) // go forward
        {
            direction = transform.forward;
            total += direction;

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal_P2") < 0) // turns counter-clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
            totalRotation -= turnSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal_P2") > 0) // turns clockwise
        {
            angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
            totalRotation += turnSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical_P2") < 0) // go backward
        {
            direction = -transform.forward;
            total += direction;
        }

    }
}
>>>>>>> fc8b2e6678e2706fc9a10ea57b6ed96e3b96f88c
