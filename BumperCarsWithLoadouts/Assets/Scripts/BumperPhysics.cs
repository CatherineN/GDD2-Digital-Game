using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperPhysics : VehicleMovement
{
    public int playerID;//which player the script is on
    public float frictionCoef = .5f;
    public float frictionForce = 2f;
    public float impactForce = 10.0f;

    private bool collidedThisFrame;

    // Use this for initialization
    public override void Start()
    {
        Collider[] col = gameObject.GetComponents<Collider>();
        Physics.IgnoreCollision(col[0], col[1], true);
        Physics.IgnoreCollision(col[1], col[2], true);
        Physics.IgnoreCollision(col[0], col[2], true);

        collidedThisFrame = false;

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
        ApplyFriction(CalculateCoefficientFriction(frictionCoef, frictionForce));

        collidedThisFrame = false;
    }

    private void ManageCollision(UnityEngine.Collision collision)
    {
        // get the other collider
        Transform otherT = collision.collider.transform;
        // calculate the force direction
        Vector3 forceDir = transform.position - otherT.position;
        // get the projection
        float impact = Vector3.Dot(velocity.normalized, forceDir.normalized);
        // factor in mass
        impact = impact * (rb.mass / collision.collider.GetComponent<Rigidbody>().mass);
        if(impact == 0)
        {
            impact = 0.0001f;
        }
        // get the resultant force
        Vector3 resultantForce = forceDir.normalized * impact * impactForce;
        Debug.Log(resultantForce);
        // correct for being inside the rigidbody
        //ApplyForce(-velocity);
        transform.position += -velocity;
        // apply the force to the other object
        collision.gameObject.GetComponent<BumperPhysics>().ApplyForce(resultantForce);
        ApplyForce(-resultantForce * 0.1f);
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

            if (vt > 0) // go forward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (vt < 0) // turns counter-clockwise
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
            if (Input.GetKey(KeyCode.W)) // go forward
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
            if (Input.GetKey(KeyCode.S)) // go backward
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
