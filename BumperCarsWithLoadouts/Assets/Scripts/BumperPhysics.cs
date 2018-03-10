using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperPhysics : VehicleMovement
{
    public int playerID;//which player the script is on
    public float frictionCoef = .5f;
    public float frictionForce = 2f;
    public float impactForce = 10.0f;
    public float gravity = 1.0f;

    private bool collidedThisFrame;
    private bool falling;
    private float carHeight = 0.5f;

    // Use this for initialization
    public override void Start()
    {
        Collider[] col = gameObject.GetComponents<Collider>();
        Physics.IgnoreCollision(col[0], col[1], true);
        Physics.IgnoreCollision(col[1], col[2], true);
        Physics.IgnoreCollision(col[0], col[2], true);

        collidedThisFrame = false;
        falling = false;
        physicsDebug = true;

        base.Start();
    }

    /// <summary>
    /// Takes player input and calculates the forces acting on it accordingly
    /// Needs a max force**
    /// </summary>
    protected override void CalcSteeringForces()
    {
        // this is for our rotation
        angleToRotate = Quaternion.Euler(0, 0, 0);

        // make sure we're on the ground
        CheckFalling();
        
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

        if(falling)
        {
            // apply gravity
            total += Vector3.down * gravity;
        }

        
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(angleToRotate * direction, transform.forward, Time.deltaTime * 0.5f);
        if (!falling)
            ApplyFriction(CalculateCoefficientFriction(frictionCoef, frictionForce));

        collidedThisFrame = false;
    }

    private void ManageCollision(UnityEngine.Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CarToCarCollision(collision);
        }
    }

    private void CarToCarCollision(UnityEngine.Collision collision)
    {
        // get the other collider
        Transform otherT = collision.collider.transform;
        // calculate the force direction
        Vector3 forceDir = transform.position - otherT.position;
        // get the projection
        float impact = Vector3.Dot(velocity, forceDir.normalized);
        // factor in mass
        impact = impact * (rb.mass / collision.collider.GetComponent<Rigidbody>().mass);
        if (impact == 0)
        {
            impact = 0.0001f;
        }
        // get the resultant force
        Vector3 resultantForce = forceDir.normalized * impact * impactForce;
        // correct for being inside the rigidbody
        //ApplyForce(-velocity);
        transform.position += (-velocity * 1.1f);
        // apply the force to the other object
        collision.gameObject.GetComponent<BumperPhysics>().ApplyForce(resultantForce);
        ApplyForce(-resultantForce * 0.5f);
    }

    private void PlaceCarOnTerrain(RaycastHit hit)
    {
        transform.position = new Vector3(transform.position.x, hit.point.y + carHeight, transform.position.z);
        /*eulerToRotate += ((transform.position + transform.up) - (transform.position + hit.normal));
        angleToRotate = Quaternion.Euler(eulerToRotate);/*/
        targetUp = hit.normal;
    }

    private void CheckFalling()
    {
        // if there is nothing under the car, we are falling
        RaycastHit hit;
        if(!Physics.Raycast(transform.position, -transform.up, out hit,1.0f))
        {
            falling = true;
            lockRotation = false;
        }
        else
        {
            falling = false;
            lockRotation = true;
            PlaceCarOnTerrain(hit);
        }
    }

    

    /// <summary>
    /// Calculates Forces based upon player input
    /// WASD or first joystick to control player 1
    /// </summary>
    void GetInputPlayer1()
    {
        //only check for joystick input if there are joysticks plugged in
        if (Input.GetJoystickNames().Length != 0)
        {
            //determine how far player is pushing joystick/trigger
            float hr = Input.GetAxis("Horizontal_P1");
            float vt = Input.GetAxis("Vertical_P1");

            //use spectrum of input strength to reflect in how fast change is in acceleration & turning

            if (vt > 0 && !falling) // go forward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (vt < 0 && !falling) // turns counter-clockwise
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (hr < 0) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
            if (hr > 0) // go backward
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && !falling) // go forward
            {
                direction = transform.forward;
                total += direction;

            }
            if (Input.GetKey(KeyCode.A)) // turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
                totalRotation -= turnSpeed;
            }
            if (Input.GetKey(KeyCode.D)) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
                totalRotation += turnSpeed;
            }
            if (Input.GetKey(KeyCode.S) && !falling) // go backward
            {
                direction = -transform.forward;
                total += direction;
            }
        }


    }

    /// <summary>
    /// Calculates Forces based upon player input
    /// Arrow Keys or second joystick to control player 2
    /// </summary>
    void GetInputPlayer2()
    {
        //only check for joystick input if there are joysticks plugged in
        if (Input.GetJoystickNames().Length != 0)
        {
            //determine how far player is pushing joystick/trigger
            float hr = Input.GetAxis("Horizontal_P2");
            float vt = Input.GetAxis("Vertical_P2");

            //use spectrum of input strength to reflect in how fast change is in acceleration & turning

            if (vt > 0) // go forward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (vt < 0)// go backward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (hr < 0)// turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
            if (hr > 0)// turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) // go forward
            {
                direction = transform.forward;
                total += direction;

            }
            if (Input.GetKey(KeyCode.LeftArrow)) // turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
                totalRotation -= turnSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow)) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
                totalRotation += turnSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow)) // go backward
            {
                direction = -transform.forward;
                total += direction;
            }
        }

    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!collidedThisFrame)
        {
            collidedThisFrame = true;
            ManageCollision(collision);
        }
    }
}
