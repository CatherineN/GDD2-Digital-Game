using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : VehicleMovement {

    public float detectRadius; //--5-- distance from the AI where player will be sought out
    public float playerWeight; //--2-- how much preference is given to players when ai decides who to attack -- not being used
    public float nearEdgeWeight; //--3-- how much being near the edge of the map plays into ai's decision on who to attack --  not being used
    public float boundsWeight; //--2-- how strong the force is to keep the AI on the platform
    public float ignoreBoundsArea; //--.75-- the percent of the radius of the arena where the ai will ignore the edge
    public bool isDead = false;

    private GameObject target; //the gameobject that the agent is chasing
    private Vector3 adjustmentTarget; //the point in space that the AI will go to in order to build up speed before bumping the target
    private bool tooClose = false;//determines whether or not the AI is too close to its target to bump effectively

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
        CheckAlive();

        // this is for our rotation
        //angleToRotate = Quaternion.Euler(0, 0, 0);

        // this is the sum of all the forces
        total = Vector3.zero;

        //actual adding of forces to the total force
        //set the target of the Autonomous Agent
        SetTarget();
        //check how close target and distance self if extremely close
        //Vector3 adjustForce = AdjustPosition();
        AdjustPosition();
        /*if (adjustForce != Vector3.zero)
        {
            Debug.Log("adjusting");
            total += 3 * adjustForce;
        }*/
        if(tooClose == true)
        {
            total += Seek(adjustmentTarget);
        }
        //seek its target
        else if (target != null)
        {
            //add seeking force to ultimate force
            total += 3 * Pursue(target);
        }
        //add force away from edges
        total += boundsWeight * StayInBounds(ignoreBoundsArea);

        //clamp the total force to maxForce
        total = Vector3.ClampMagnitude(total, maxForce);
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(direction, total.normalized, Time.deltaTime * 0.5f);
        transform.forward = direction;

        ApplyFriction(CalculateCoefficientFriction(0.5f, 2.0f));

    }

    
    private void SetTarget()
    {
        Debug.Log("Finding Target");
        Vector3 closest = new Vector3(10000, 10000, 10000);
        //compare distance between this car and every other car
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
    /// <summary>
    /// Checks whether the AI car has fallen off the arena
    /// </summary>
    private void CheckAlive()
    {
        if (gameObject.transform.position.y < -10)
        {
            isDead = true;
            tag = "Dead";
        }
    }

    private void AdjustPosition()
    {
        //if the ai is too close to its target, it won't be able to actually bump it, so move it back
        if((target.transform.position - gameObject.transform.position).magnitude < 5f && velocity.magnitude < .1f)
        {
            //return Seek(gameObject.transform.position - gameObject.transform.forward *10);
            tooClose = true;
            adjustmentTarget = gameObject.transform.position + (target.transform.position - gameObject.transform.forward*3);
        }
        //return Vector3.zero;
        if((gameObject.transform.position - adjustmentTarget).magnitude < 1f)
        tooClose = false;
    }
}
