using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScawySystem : VehicleMovement {

    public enum AIBehavior
    {
        Stalk,
        Attack,
        Retreat,
        Fatality
    }

    
    //target reference
    public GameObject[] nodes;
    public GameObject target;
    private int nodeNum;
    public float nodeDist;
    public float speed;
    public float scalar;
    public float minDist;

    public float targetRadius;

    public AIBehavior currentState = AIBehavior.Stalk;


    // Use this for initialization
    public override void Start () {
        nodeNum = 0;
        base.Start();

        maxSpeed = speed;
	}
	
	// Update is called once per frame
	 public override void Update () {
        total = Vector3.zero;

        switch (currentState)
        {
            case AIBehavior.Stalk:
                Stalk();
                TransitionStalkAtk();
                TransitionStalkFat();
                break;
            case AIBehavior.Attack:
                Attack();
                TransitionAtkFat();
                TransitionAtkRtr();
                break;
            case AIBehavior.Retreat:
                Retreat();
                TransitionRtrStalk();
                break;
            case AIBehavior.Fatality:
                Fatality();
                TransitionFatRtr();
                break;
        }

       

        base.Update();
    }

    bool ChangeNode()
    {
        //if the ai gets close to a node go to the next node
        Vector3 dist = gameObject.transform.position - nodes[nodeNum].transform.position;
        float distance = dist.sqrMagnitude;

        if(distance < nodeDist)
        {
            nodeNum++;
            nodeNum = nodeNum % 5;
            return true;
        }
        return false;
    }

    /// <summary>
    /// AI moves in predefined paths around arena
    /// </summary>
    void Stalk()
    {
        //switch the node that the AI is seeking currently
        ChangeNode();

        Vector3 seekingForce = Seek(nodes[nodeNum].transform.position);
        Debug.Log(seekingForce);
        total += seekingForce;        
    }

    protected override void CalcSteeringForces()
    {
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + total * 10, Color.black);
        total = Vector3.ClampMagnitude(total, maxForce);
        ApplyForce(total);
    }

    /// <summary>
    /// Initial attack against the targeted bumper car
    /// Only includes Bumping right now, no loadouts
    /// </summary>
    void Attack()
    {
        Vector3 futurePos = PursueTarget();

        Vector3 attackForce = Seek(futurePos);
        total += attackForce;

    }

    /// <summary>
    /// Unique Pursue method that attempts to find a point past the targeted car
    /// </summary>
    /// <returns>Point past the car</returns>
    Vector3 PursueTarget()
    {
       
        //find a point in front of target
        Vector3 dir = target.transform.position - gameObject.transform.position;
        //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + dir, Color.red);
        dir.Normalize();
        Vector3 futurePos = target.GetComponent<VehicleMovement>().Velocity;
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + futurePos * 10, Color.blue);
        futurePos.Normalize();

        Vector3 targetPos = ((dir + futurePos) * scalar) + target.transform.position;

        //Debug.DrawLine(gameObject.transform.position, targetPos);

        return targetPos;
    }

    /// <summary>
    /// Transition the AI from Stalking behavior to Attacking behavior
    /// Only changes the AI behavior if the AI can find a targetable car that is at least a certain distance away
    /// </summary>
    void TransitionStalkAtk()
    {
        //checks for a target that is greater than a certain distance away
        Vector3 farthest = new Vector3(0, 0, 0);
        float farthestDist = farthest.sqrMagnitude;
        //target = null;
        Vector3 dir = target.transform.position - gameObject.transform.position;
        float tempDist = dir.sqrMagnitude;

        //compare distance between this car and every other car
        //for (int i = 0; i < cM.Cars.Count; ++i)
        //{
        //    //get distance between
        //    Vector3 temp = cM.Cars[i].transform.position - position;
        //    tempDist = temp.sqrMagnitude;

        //    //check if its farther
        //    if(tempDist > farthestDist)
        //    {
        //        farthestDist = tempDist;
        //        target = cM.Cars[i];
        //    }
        //}
        

        
        //state transition
        if(tempDist > minDist)
        {
            //total = Vector3.zero;
            Debug.Log(tempDist);
            currentState = AIBehavior.Attack;
        }
    }

    /// <summary>
    /// Transitions between Retreat behavior and Stalk behavior
    /// Occurs when the AI reaches the node farthest from when it transitioned into retreat
    /// </summary>
    void TransitionRtrStalk()
    {
        if(ChangeNode())
        {
            currentState = AIBehavior.Stalk;
        }
    }

    /// <summary>
    /// After attacking the car should retreat back towards the middle of the arena away from the edge
    /// Seeks the node on the set stalking path that is furthest away
    /// </summary>
    void Retreat()
    {
        Vector3 seekingForce = Seek(nodes[nodeNum].transform.position);
        total += seekingForce;
    }

    /// <summary>
    /// Transition between the Attack and Retreat behaviors
    /// Occurs once the car reaches the point past their target or hits the target
    /// Check if they reached within a certain radius, perhaps a cone of acceptable area
    /// </summary>
    void TransitionAtkRtr()
    {
        //check if within acceptable area
        if((position - target.transform.position).magnitude < targetRadius)
        {
            currentState = AIBehavior.Retreat;
            GameObject seekNode = Utility.FindFurthestObject(nodes, gameObject);
            //need a way to check what the nodeNum is for Stalk when it finishes retreating
        }
    }

    /// <summary>
    /// State of following the targeted car and repeatedly hitting it until it falls off the edge
    /// </summary>
    void Fatality()
    {
        
    }

    /// <summary>
    /// Transition between the Attack and Fatality behaviors
    /// Occurs if the target's position after attack is near the edge of a fall-off point
    /// </summary>
    void TransitionAtkFat()
    {
        
    }

    /// <summary>
    /// Transition between the Stalk and Fatality behaviors
    /// Occurs if the selected target is near the edge of a fall-off point
    /// </summary>
    void TransitionStalkFat()
    {
        
    }

    /// <summary>
    /// Transition between the Fatality and Retreat behaviors
    /// Occurs when the target car has been knocked off the arena
    /// Check if the target has become inactive
    /// </summary>
    void TransitionFatRtr()
    {

    }
}
