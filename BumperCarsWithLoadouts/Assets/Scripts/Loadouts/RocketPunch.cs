using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPunch : MonoBehaviour {

    // Use this for initialization
    //variables for speed and velocity
    private float speed;
    public Vector3 velocity;
    public Vector3 direction;
    int timer;
    private List<GameObject> carList;
    private bool targetLocked;
    private Vector3 carToSeek;
    public int parentCarID;
    private Vector3 acceleration;
    void Start ()
    {
        speed = 1.1f;
        carList = GameObject.Find("SceneManager").GetComponent<CarManager>().Cars;
        carToSeek = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        targetLocked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        Vector3 ultimaForce = Vector3.zero;
        if (!targetLocked)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                Debug.Log("shiiiiiiiiiit");

                if (parentCarID == carList[i].GetComponent<Player>().playerID)
                {
                    Debug.Log("waaaaaaaaaaaaaa");

                    //carToSeek = carList[i+1].transform.position;
                    continue;
                }
                GameObject tempTarget = carList[i];
                if ((tempTarget.transform.position - transform.position).magnitude < (carToSeek - transform.position).magnitude)
                {
                    carToSeek = tempTarget.transform.position;
                    Debug.Log("FUUUUUGGG");
                }
                    
                targetLocked = true;
            }
        }
        
        ultimaForce += Seek(carToSeek);
        acceleration = ultimaForce;
        velocity += acceleration;
        //transform.forward = direction;
        transform.position += velocity;
        if (timer == 80)
            Destroy(gameObject);
        timer++;
    }
    protected Vector3 Seek(Vector3 targetPosition)
    {
        //Step 1: Calculate desired velocity : From myself to target
        Vector3 desiredVelocity = targetPosition - transform.position;
        //Step 2: Scale the max speed : limit steering force to vehicle capabilities
        //desiredVelocity = Vector3.ClampMagnitude (desiredVelocity, maxSpeed);
        desiredVelocity.Normalize();
        desiredVelocity *= speed;
        //Step 3: Calculate steering force : steering force = desired - current velocity
        Vector3 steeringForce = desiredVelocity - velocity;
        //Step 4: Return the force so it can be applied to this vehicle
        return steeringForce;
    }
}
