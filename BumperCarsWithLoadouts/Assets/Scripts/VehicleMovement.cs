using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// READ THE HHHfUCKING COMMENTS.
//
public abstract class VehicleMovement : MonoBehaviour {

    // **For this, we are assuming that the mass of the bumper car will be 1**
    // variables
    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;
    protected Vector3 desiredVelocity;
    protected Vector3 acceleration;
    protected Vector3 total;//total force acting on the gameobject every frame
    protected Quaternion angleToRotate;//how far to rotate this frame

    public float maxForce;
    protected float maxSpeed = .75f;
    protected float minSpeed = .4f;
    protected float turnSpeed = 1.9f;
    protected float totalRotation = 0; // add or subtract 1 when rotating the bumper car
    protected float damping = 5; // this will slow down the bumper car as we turn

    // Use this for initialization
    public virtual void Start () {
        position = transform.position;
	}

    // public getter for velocity
    // Used by Collsion.cs
    public Vector3 Velocity
    {
        get { return velocity; }
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
    protected abstract void CalcSteeringForces();


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
    public void ApplyForce(Vector3 force)
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
}
