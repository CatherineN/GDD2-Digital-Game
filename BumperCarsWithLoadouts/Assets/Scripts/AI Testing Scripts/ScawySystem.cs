using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScawySystem : MonoBehaviour {

    public enum State
    {
        Stalk,
        Attack,
        Scawed,
    }

    //attributes
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float mass;
    public float maxSpeed;

    //target reference
    public GameObject[] nodes;
    public GameObject target;
    private int targetNum;
    public float minDist;
    public bool isAttacking;

    public float timer;
    public float startTimer;
    public float targetRadius;




    // Use this for initialization
    void Start () {
        targetNum = 0;
        isAttacking = false;
        timer = startTimer;
	}
	
	// Update is called once per frame
	void Update () {
        Stalk();
        UpdatePosition();
        SetTransform();

        if (!isAttacking)
        {
            ChangeTarget();
        }

        if(timer < 0)
        {
            Attack();
        }

        timer--;
    }

    void ChangeTarget()
    {
        //if the ai gets close to a node go to the next node
        Vector3 dist = gameObject.transform.position - nodes[targetNum].transform.position;
        float distance = dist.sqrMagnitude;

        if(distance < minDist)
        {
            targetNum++;
            targetNum = targetNum % 5;
        }
    }

    //AI moves in circles around arena
    void Stalk()
    {
        Vector3 seekingForce = Seek(nodes[targetNum].transform.position);
        ApplyForce(seekingForce);
    }

    Vector3 Seek(Vector3 targetPosition)
    {
        //step 1, calculate a desired velocity
        //which is from myself to my target's position
        Vector3 desiredVelocity = targetPosition - position;

        //step 2, Scale to the maximum speed
        //this limits the steering force to the capabilities of this vehicle
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);

        //alternate way = normalize the desiredVelocity and then * maxSpeed
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        //step 3, calculate the steering force
        //steeringForce = desired - current
        Vector3 steeringForce = desiredVelocity - velocity;

        //step 4, return the force so it can be applied to the vehicle
        return steeringForce;
    }

    void ApplyForce(Vector3 force)
    {
        //accumulate the forces
        acceleration += force / mass;
    }

    void UpdatePosition()
    {
        position = transform.position;

        //step 1, add acceleration to velocity + multiply by time
        velocity += acceleration * Time.deltaTime;

        //step 2, add velocity to position
        position += velocity * Time.deltaTime;

        //step 3, reset acceleration vector
        acceleration = Vector3.zero;

        //step 4, set my own direction vector
        //based on current velocity
        direction = velocity.normalized;
    }

    void SetTransform()
    {
        gameObject.transform.forward = direction;
        gameObject.transform.position = position;
    }

    void Attack()
    {
        isAttacking = true;

        Vector3 futurePos = PursueTarget();

        Vector3 dist = gameObject.transform.position - target.transform.position;
        float distance = dist.sqrMagnitude;

        if (distance > targetRadius)
        {
            Vector3 seekingForce = Seek(futurePos);
            ApplyForce(seekingForce);
        }
        else
        {
            //attack is over
            timer = startTimer;
            isAttacking = false;
        }
    }
        Vector3 PursueTarget()
    {
        //get reference to target
        //testing with one target for now

        //find a point in front of target
        Vector3 futurePos = target.transform.position + target.transform.forward * 3;
        return futurePos;

    }

}
