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
    private bool looping = false;

    public float minVelocity; //the lowest possible velocity to make a significant bump -- .25
    //public float checkRadius;// the edge of glooooooooooooooooooooory or where to determine if too close with low velocity -- 3
    public float minBestBumpDistance; //get the ratio between the distance and the minimum best bump distance

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
        //seek its target
        if (target != null)
        {
            //add seeking force to ultimate force
            total += 3 * Pursue(target);
        }
        //add force away from edges
        total += boundsWeight * StayInBounds(ignoreBoundsArea);

        //clamp the total force to maxForce
        total = Vector3.ClampMagnitude(total, maxForce);
        total.y = 0;
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
        GameObject closestObject = null;
        target = null;
        //compare distance between this car and every other car
        for (int i = 0; i < cM.Cars.Count; ++i)
        {
            //get distance between
            Vector3 temp = cM.Cars[i].transform.position - position;

            //get the ratio between the distance and the minimum best bump distance
            float ratio = minBestBumpDistance / temp.magnitude;

            //get the future velocity of the AI car when it reaches the other car's position
            //Vector3 futVelocity = GetFutureVelocity(cM.Cars[i].transform.position);

            //compare to see if is smaller than stored
            if (temp.magnitude < closest.magnitude)
            {
                closestObject = cM.Cars[i]; //always set the closest object even if they can't build up to effective velocity to allow for repositioning

                //determine if they will reach adequate velocity and therefore adequate force by the time they arrive at the target's position
                if (velocity.magnitude * ratio < minVelocity)//make a fail check that continues to next target in loop if they won't reach necessary velocity
                {
                    Debug.Log(gameObject.name + "'s gotta go faster");
                    continue;
                }

                //only update the target and closest object if AI can reach effective velocity
                closest = temp;
                target = cM.Cars[i];
            }
            else if(cM.Cars[i].tag == "Player" && temp.magnitude < detectRadius)
            {
                closestObject = cM.Cars[i];//always set the closest object even if they can't build up to effective velocity to allow for repositioning

                //determine if they will reach adequate velocity and therefore adequate force by the time they arrive at the target's position
                if (velocity.magnitude *ratio < minVelocity)//make a fail check that continues to next target in loop if they won't reach necessary velocity
                {
                    Debug.Log(gameObject.name + "'s gotta go faster");
                    continue;
                }

                //only update the target if AI can reach effective velocity
                target = cM.Cars[i];
                break;
            }
        }
        if(target == null)// || looping == true)//do the loop around behavior to build up speed before chasing a target
        {
            Vector3 loopPoint = Vector3.zero + (closestObject.transform.position - Vector3.zero);
            total += 3 *Seek(loopPoint);
            //looping = true;
            Debug.Log(gameObject.name + " is looping around for another go");
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
            adjustmentTarget = gameObject.transform.position + (target.transform.position - gameObject.transform.forward*5);
        }
        //return Vector3.zero;
        if((gameObject.transform.position - adjustmentTarget).magnitude < .01f)
        tooClose = false;
    }

    private Vector3 GetFutureVelocity(Vector3 tarPos)
    {
        //velocity += maxforce (can't do times deltaTime, so instead need the average framerate and then 1/that number)
        //as many times as that takes to get to the target's location/cover the distance between them and that is the future velocity
        float timePerFrame = Time.realtimeSinceStartup/Time.frameCount;//average time per frame during the project

        Vector3 futVelocity = velocity;
        Vector3 tempPos = transform.position;
        Vector3 dirToTar = (tarPos - tempPos).normalized;

        int i = 0;//test

        //assume max acceleration/force and know the distance between the two, know current velocity, want to know velocity when you reach it, know the time during which accelertion is added

        while(i < 10)//Vector3.Distance(tempPos, tarPos) > futVelocity.magnitude)//breaking the AI code rn, infinite loop or at least it runs too many times to stick to 30fps
        {
            futVelocity += Seek(tarPos);//(maxForce * dirToTar)*timePerFrame;
            futVelocity = Vector3.ClampMagnitude(futVelocity, maxSpeed);
            tempPos += futVelocity;

            //takes AI about 1.333 seconds to get to max speed

            //v2 = a(t2 - t1) + v1
            //a = .6, v1 = velocity, t1 = 0 or realTimeSinceStartup, t2 = ?, v2 = !?

            //Debug.Log("Temp Position: " + tempPos);
            Debug.Log("Future Velocity: " + futVelocity.magnitude);
            //Debug.Log("Distance left: " + Vector3.Distance(tempPos, tarPos));

            i++;
        }
        //Debug.Log("Here");
        return futVelocity;
    }
}
