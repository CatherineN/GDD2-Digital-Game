using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperPhysics : VehicleMovement
{
    public int playerID;//which player the script is on
    public float frictionCoef = .5f;
    public float frictionForce = 2f;
    public float impactForce = 10.0f;
    public float gravity = 1.0f;
    public float cannonImpact = 1f;
    public GameObject stage;

    private bool collidedThisFrame;
    private bool falling;
    private float carHeight = 0.5f;

    private List<Collider> colList;

    // Use this for initialization
    public override void Start()
    {
        Collider[] col = gameObject.GetComponents<Collider>();
        Physics.IgnoreCollision(col[0], col[1], true);
        Physics.IgnoreCollision(col[1], col[2], true);
        Physics.IgnoreCollision(col[0], col[2], true);

        collidedThisFrame = false;
        falling = false;
        physicsDebug = true;

        colList = new List<Collider>();

        for(int i = 0; i < stage.transform.childCount; i++)
        {
            colList.Add(stage.transform.GetChild(i).GetComponent<Collider>());
        }

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

        //gameObject.GetComponent<Renderer>().material.color = Color.green;

        // make sure we're on the ground
        CheckFalling();
        
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

        if(falling)
        {
            // apply gravity
            total += Vector3.down * gravity;
        }

        
        // apply the sum of the forces to our bumper car
        ApplyForce(total);

        direction = Vector3.Lerp(angleToRotate * direction, transform.forward, Time.deltaTime * 0.5f);
        if (!falling)
            ApplyFriction(CalculateCoefficientFriction(frictionCoef, frictionForce));

        collidedThisFrame = false;

        
    }

    private void ManageCollision(UnityEngine.Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CarToCarCollision(collision);
        }
        else if(collision.gameObject.tag == "Slope")
        {
            CarToSlope(collision);
        }
        else
        {
            Ray r = new Ray(transform.position, (collision.contacts[0].point - transform.position).normalized);
            RaycastHit hit;
            collision.collider.Raycast(r, out hit, float.MaxValue);
            CarToTerrainCollision(collision, hit);
        }
    }

    private void CarToCarCollision(UnityEngine.Collision collision)
    {
        // get the other collider
        Transform otherT = collision.collider.transform;
        // calculate the force direction
        Vector3 forceDir = transform.position - otherT.position;
        // get the projection
        float impact = Vector3.Dot(velocity, forceDir.normalized);
        // factor in mass
        impact = impact * (rb.mass / collision.collider.GetComponent<Rigidbody>().mass);
        if (impact == 0)
        {
            impact = 0.0001f;
        }
        // get the resultant force
        Vector3 resultantForce = forceDir.normalized * impact * impactForce;
        // correct for being inside the rigidbody
        //ApplyForce(-velocity);
        transform.position += (-velocity * 1.1f);
        // apply the force to the other object
        collision.gameObject.GetComponent<BumperPhysics>().ApplyForce(resultantForce);
        ApplyForce(-resultantForce * 0.5f);
    }

    private void CarToTerrainCollision(UnityEngine.Collision collision, RaycastHit hit)
    {
        // calculate the force direction
        Vector3 forceDir = collision.contacts[0].point - transform.position;
        /*RaycastHit hit;
        if (Physics.Raycast(collision.contacts[0].point + transform.up, -transform.up, out hit, 1.0f))
        {
            if(hit.normal != transform.up)
            {
                targetUp = hit.normal;
                return;
            }
        }*/
        // get the projection
        float impact = Vector3.Dot(velocity, forceDir.normalized);
        if (impact == 0)
        {
            impact = 0.0001f;
        }
        // get the resultant force
        Vector3 resultantForce = forceDir.normalized * impact * 100f;
        Vector3 reflectedForce = Vector3.Reflect(resultantForce, hit.normal);
        transform.position += (-(velocity.sqrMagnitude * hit.normal * 1.5f));
        // apply the force to the player
        ApplyForce(reflectedForce);
    }

    private void CarToSlope(UnityEngine.Collision collision)
    {
        // check if we can be on a slope
        Ray r = new Ray(transform.position, (collision.contacts[0].point - transform.position).normalized);
        RaycastHit hit;
        collision.collider.Raycast(r, out hit, float.MaxValue);
        if(Vector3.Angle(hit.normal, transform.up) > 89f)
        {
            CarToTerrainCollision(collision, hit);
        }
    }

    private void PlaceCarOnTerrain(RaycastHit hit)
    {
        /*RaycastHit hit1;
        Physics.Raycast(transform.position + transform.forward, -transform.up, out hit1, 1.0f);
        RaycastHit hit2;
        Physics.Raycast(transform.position - transform.forward, -transform.up, out hit2, 1.0f);
        RaycastHit bestHit;
        float angle1 = Vector3.SignedAngle(transform.up, hit.normal, transform.up);
        float angle2 = Vector3.SignedAngle(transform.up, hit1.normal, transform.up);
        float angle3 = Vector3.SignedAngle(transform.up, hit2.normal, transform.up);
        if (angle1 > angle2) bestHit = hit;
        else bestHit = hit1;
        if (angle2 < angle3) bestHit = hit2;*/
        transform.position = new Vector3(transform.position.x, hit.point.y + carHeight, transform.position.z);
        //transform.position = hit.point + hit.normal.normalized * carHeight;
        /*eulerToRotate += ((transform.position + transform.up) - (transform.position + hit.normal));
        angleToRotate = Quaternion.Euler(eulerToRotate);/*/
        targetUp = hit.normal;
    }

    private void CheckFalling()
    {
        // if there is nothing under the car, we are falling
        Ray r = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        foreach(Collider c in colList)
        {
            if (c.Raycast(r, out hit, carHeight /*+ velocity.sqrMagnitude*/))
            {
                falling = false;
                PlaceCarOnTerrain(hit);
                return;
            }
        }

        // no hit
        falling = true;
    }

    public void ProjectileHit(Collider other)
    {
        ApplyForce(other.GetComponent<Cannonball>().velocity.normalized * cannonImpact);
        GameObject pSystem = transform.GetChild(4).gameObject;//HitByCannon Particle effect
        pSystem.transform.localPosition = transform.InverseTransformPoint(other.transform.position);
        StartCoroutine(Hit());
        Destroy(other.gameObject);
    }

    public void PunchHit(Collider other)
    {
        ApplyForce(other.GetComponent<RocketPunch>().velocity.normalized * (cannonImpact * 1.5f));
        Destroy(other.gameObject);
    }

    private void ResolveCollision(UnityEngine.Collision collision)
    {
        Vector3 dif = transform.TransformPoint(collision.contacts[0].thisCollider.bounds.center) - 
            collision.contacts[0].otherCollider.transform.TransformPoint(collision.contacts[0].otherCollider.bounds.center);

        transform.position += dif;
    }

    

    /// <summary>
    /// Calculates Forces based upon player input
    /// WASD or first joystick to control player 1
    /// </summary>
    void GetInputPlayer1()
    {
        //only check for joystick input if there are joysticks plugged in
        if (Input.GetJoystickNames().Length != 0)
        {
            //determine how far player is pushing joystick/trigger
            float hr = Input.GetAxis("Horizontal_P1");
            float vt = Input.GetAxis("Vertical_P1");

            //use spectrum of input strength to reflect in how fast change is in acceleration & turning

            if (vt > 0 && !falling) // go forward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (vt < 0 && !falling) // turns counter-clockwise
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (hr < 0) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
            if (hr > 0) // go backward
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && !falling) // go forward
            {
                direction = transform.forward;
                total += direction;

            }
            if (Input.GetKey(KeyCode.A)) // turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
                totalRotation -= turnSpeed;
            }
            if (Input.GetKey(KeyCode.D)) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
                totalRotation += turnSpeed;
            }
            if (Input.GetKey(KeyCode.S) && !falling) // go backward
            {
                direction = -transform.forward;
                total += direction;
            }
        }


    }

    /// <summary>
    /// Calculates Forces based upon player input
    /// Arrow Keys or second joystick to control player 2
    /// </summary>
    void GetInputPlayer2()
    {
        //only check for joystick input if there are joysticks plugged in
        if (Input.GetJoystickNames().Length != 0)
        {
            //determine how far player is pushing joystick/trigger
            float hr = Input.GetAxis("Horizontal_P2");
            float vt = Input.GetAxis("Vertical_P2");

            //use spectrum of input strength to reflect in how fast change is in acceleration & turning

            if (vt > 0) // go forward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (vt < 0)// go backward
            {
                direction = transform.forward;
                total += direction * vt;
            }
            if (hr < 0)// turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
            if (hr > 0)// turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed * hr, 0);
                totalRotation += turnSpeed * hr;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) // go forward
            {
                direction = transform.forward;
                total += direction;

            }
            if (Input.GetKey(KeyCode.LeftArrow)) // turns counter-clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y - turnSpeed, 0);
                totalRotation -= turnSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow)) // turns clockwise
            {
                angleToRotate = Quaternion.Euler(0, angleToRotate.y + turnSpeed, 0);
                totalRotation += turnSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow)) // go backward
            {
                direction = -transform.forward;
                total += direction;
            }
        }

    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!collidedThisFrame)
        {
            collidedThisFrame = true;
            ManageCollision(collision);
        }
    }

    IEnumerator Hit()
    {
        GameObject p = transform.GetChild(4).gameObject;//HitByCannon Particle effect
        ParticleSystem pSystem = p.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = pSystem.emission;
        for (int i = 0; i < 20; i++)
        {
            em.enabled = true;
            yield return null;
        }
        em.enabled = false;
    }

    public void NotifyColliderDelete(Collider collider)
    {
        colList.Remove(collider);
    }
}
