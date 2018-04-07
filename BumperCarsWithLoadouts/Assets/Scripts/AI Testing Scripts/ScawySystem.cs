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
                break;
            case AIBehavior.Attack:
                Attack();
                break;
            case AIBehavior.Retreat:
                break;
            case AIBehavior.Fatality:
                break;
        }

       

        base.Update();
    }

    void ChangeNode()
    {
        //if the ai gets close to a node go to the next node
        Vector3 dist = gameObject.transform.position - nodes[nodeNum].transform.position;
        float distance = dist.sqrMagnitude;

        if(distance < nodeDist)
        {
            nodeNum++;
            nodeNum = nodeNum % 5;
        }
    }

    //AI moves in circles around arena
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

    
    void Attack()
    {
        Vector3 futurePos = PursueTarget();

        Vector3 attackForce = Seek(futurePos);
        total += attackForce;

    }

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

}
