using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : VehicleMovement {

    public float detectRadius; //--5-- distance from the AI where player will be sought out
    public float playerWeight; //--2-- how much preference is given to players when ai decides who to attack
    public float nearEdgeWeight; //--3-- how much being near the edge of the map plays into ai's decision on who to attack
    public float boundsWeight; //--2-- how strong the force is to keep the AI on the platform

    private GameObject target; //the gameobject that the agent is chasing

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

        //actual adding of forces to the total force
        //set the target of the Autonomous Agent
        SetTarget();
        //seek its target
        if (target != null)
        {
            //add seeking force to ultimate force
            total += 3 * Pursue(target);
        }
        //add force away from edges
        total += boundsWeight * StayInBounds();

        //clamp the total force to maxForce
        total = Vector3.ClampMagnitude(total, maxForce);
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(angleToRotate * direction, transform.forward, Time.deltaTime * 0.5f);
        ApplyFriction(CalculateCoefficientFriction(0.5f, 2.0f));

    }

    
    private void SetTarget()
    {
        Debug.Log("Finding Target");
        Vector3 closest = new Vector3(10000, 10000, 10000);
        //compare distance between zombie and each human
        for (int i = 0; i < cM.Cars.Count; ++i)
        {
            //get distance between
            Vector3 temp = cM.Cars[i].transform.position - position;
            //compare to see if is smaller than stored
            if (temp.magnitude < closest.magnitude && temp.magnitude != 0)
            {
                closest = temp;
                target = cM.Cars[i];
            }
            else if(cM.Cars[i].tag == "Player" && temp.magnitude < detectRadius)
            {
                target = cM.Cars[i];
                break;
            }
        }
    }
}
