using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : VehicleMovement {

    public int playerID;//which player the script is on

    // Use this for initialization
    public override void Start () {
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
        ApplyFriction(CalculateCoefficientFriction(0.5f, 2.0f));
    }

    /// <summary>
    /// Calculates Forces based upon player input
    /// WASD or first joystick to control player 1
    /// </summary>
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

    /// <summary>
    /// Calculates Forces based upon player input
    /// Arrow Keys or second joystick to control player 2
    /// </summary>
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
