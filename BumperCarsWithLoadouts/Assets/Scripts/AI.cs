using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : VehicleMovement {

    // Use this for initialization
    public override void Start()
    {
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

        //actual

        //clamp the total force to maxForce
        total = Vector3.ClampMagnitude(total, maxForce);
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(angleToRotate * direction, transform.forward, Time.deltaTime * 0.5f);
        ApplyFriction(CalculateCoefficientFriction(0.5f, 2.0f));

    }
}
