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
    private Vector3 desiredVelocity;
    protected Vector3 acceleration;
    protected Vector3 total;//total force acting on the gameobject every frame
    protected Quaternion angleToRotate;//how far to rotate this frame

    //num to determine how long between getting target's future pos
    public float timeDelay;
    //Vector3 to keep track of future position
    protected Vector3 futPos;

    public float maxForce;//.6 for the AI
    protected float maxSpeed = .75f;
    protected float minSpeed = .4f;
    protected float turnSpeed = 1.9f;
    protected float totalRotation = 0; // add or subtract 1 when rotating the bumper car
    protected float damping = 5; // this will slow down the bumper car as we turn

    protected CarManager cM;
    protected Rigidbody rb;

    protected bool lockRotation = true;

    // Use this for initialization
    public virtual void Start () {
        position = transform.position;
        cM = GameObject.Find("SceneManager").GetComponent<CarManager>();
        rb = gameObject.GetComponent<Rigidbody>();

        //set the max speed and turning speed to be affected by the mass of the car
        maxSpeed *= 50 / rb.mass;
        turnSpeed *= 50 / rb.mass;
    }

    // public getter for velocity
    // Used by Collsion.cs
    public Vector3 Velocity
    {
        get { return velocity; }
    }

    public float TotalRotation
    {
        get { return totalRotation; }
        set { totalRotation = value; }
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
        position.y = Mathf.Clamp(position.y, int.MinValue, .1f);
        //calculate direction from velocity
        //direction = velocity.normalized;
        //zero out acceleration
        acceleration = Vector3.zero;
        //calc future position at this speed over the specified time period
        futPos = position + velocity * timeDelay;
    }


    /// <summary>
    /// Sets the transform component to the local positon
    /// </summary>
    protected void SetTransform()
    {
        //set up vector equal to the seekers direction
        //transform.forward = direction;
        transform.position = position;
        if (tag == "Player" && lockRotation)
        {
            transform.rotation = Quaternion.Euler(0, totalRotation, 0);
            transform.position = new Vector3(position.x, 0.1f, position.z);
        }
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

    #region SteeringForces
    /// <summary>
    /// calculates the force to cause current object to seek a target
    /// </summary>
    /// <param name="targetPosition">position of object being saught</param>
    /// <returns>Seek force</returns>
    protected Vector3 Seek(Vector3 targetPosition)
    {
        //step 1: calculate desired velocity
        //vector from myself to the target
        desiredVelocity = targetPosition - position;
        //step 2: scale to maxspeed
        desiredVelocity = maxSpeed * desiredVelocity.normalized;
        //step 3: calculate the steering force
        desiredVelocity -= velocity;
        //step 4: return steering force
        return desiredVelocity;
    }

    /// <summary>
    /// calculates the force to cause current object to flee a target
    /// </summary>
    /// <param name="targetPosition">position of object currently fleeing from</param>
    /// <returns>Flee force</returns>
    protected Vector3 Flee(Vector3 targetPosition)
    {
        //step 1: calculate desired velocity
        //vector from myself to the target - negated so it flees
        desiredVelocity = -(targetPosition - position);
        //step 2: scale to maxspeed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        //step 3: calculate the steering force
        desiredVelocity -= velocity;
        //step 4: return steering force
        return desiredVelocity;
    }

    /// <summary>
    /// smart seek
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    protected Vector3 Pursue(GameObject target)
    {
        //seek target's future position
        return Seek(target.GetComponent<VehicleMovement>().futPos);
    }

    /// <summary>
    /// smart flee
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    protected Vector3 Evade(GameObject target)
    {
        //flee target's future position
        return Flee(target.GetComponent<VehicleMovement>().futPos);
    }

    /// <summary>
    /// Steers the gameobject back towards the center if they get too close to the edge
    /// </summary>
    /// <param name="percentSafe">What percentage of the arena will the gameobject ignore the danger of the edge</param>
    /// <returns>Force towards the center if too close to the edge, otherwise a zero vector</returns>
    protected Vector3 StayInBounds(float percentSafe)
    {
        //if agent is outside the acceptable bounds they should make their way back in
        if (position.x > cM.ArenaRadius*percentSafe || position.x < -cM.ArenaRadius* percentSafe || position.z > cM.ArenaRadius * percentSafe || position.z < -cM.ArenaRadius * percentSafe)
        {
            //seek the center of the world
            return Seek(Vector3.zero);
        }
        //if not out of bounds don't affect steering
        return Vector3.zero;
    }
    #endregion
}
