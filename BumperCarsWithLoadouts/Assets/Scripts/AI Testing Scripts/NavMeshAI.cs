using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

    //attributes 
    public float wanderRad;
    public float wanderTime;

    public NavMeshAgent agent;
    private float timer;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        timer = wanderTime;
	}
	
	// Update is called once per frame
	void Update () {
        MoveAI();
	}

    void MoveAI()
    {
        //increment time
        timer += Time.deltaTime;

        //when the timer reaches the set time
        if(timer > wanderTime)
        {
            //create a new position to move to
            Vector3 newPos = GetRandomNavSpace(transform.position, wanderRad, -1);
            //set newPos to AI destination
            agent.SetDestination(newPos);
            //reset the timer
            timer = 0;
        }
    }

    Vector3 GetRandomNavSpace(Vector3 origin, float distance, int layerMask)
    {
        //choose a random direction
        Vector3 randDir = Random.insideUnitSphere * distance;

        //set relative to this car's position
        randDir += origin;

        //NavMesh return data
        NavMeshHit navHit;

        //test if position is in the navmesh
        NavMesh.SamplePosition(randDir, out navHit, distance, layerMask);

        //return new position
        return navHit.position;
    }
}
