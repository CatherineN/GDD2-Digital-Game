using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Collision : MonoBehaviour
{
    public float impactForce;
    public float impactReduction;
    //private float stageRadius;
    public float cannonImpact;
    public LayerMask floorMask;

    private Rigidbody rb;
    private VehicleMovement p;
    private AI ai;
    private CarManager cM;

    private int collisionCount;
    private bool fallingStage = false;
    private bool wasFalling = false;

    //public float StageRadius
    //{
    //    get { return stageRadius; }
    //    set {stageRadius =value; }
    //}
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.tag == "Player")
            p = gameObject.GetComponent<Player>();
        else
            p = gameObject.GetComponent<AI>();
        collisionCount = 0;

        if (SceneManager.GetActiveScene().name == "CollapsingArena" || SceneManager.GetActiveScene().name == "VolcanoArena")
            fallingStage = true;

        cM = GameObject.Find("SceneManager").GetComponent<CarManager>();

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        collisionCount = 0;
        CheckFallOff();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15)
            return;
        if (other.gameObject.tag == "Terrain" || collisionCount > 0)
            return;

        if (other.gameObject.tag == "Projectile")
        {
            ProjectileHit(other);
            return;
        }
        if(other.gameObject.tag == "Bomb")
        {
            return;
        }
        if(other.gameObject.tag == "Paintbang")
        {
            return;
        }
        
        if(other.gameObject.tag == "RocketPunch")
        {
            PunchHit(other);
            return;
        }
        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
        VehicleMovement otherVM = other.gameObject.GetComponent<VehicleMovement>();
        
        Vector3 between = Vector3.Normalize(other.transform.position - transform.position);
        float vProjThis = Vector3.Dot(p.Velocity.normalized, between) * rb.mass;
        float vProjOther = Vector3.Dot(otherVM.Velocity.normalized, between) * otherRB.mass;

        if(vProjThis > vProjOther)
        {
            // this is doing the bumping
            Vector3 force = vProjThis * impactForce * p.Velocity;
            p.ApplyForce(-force * impactReduction);
            otherVM.ApplyForce(force);
        }
        else
        {
            // this is getting hit
            Vector3 force = vProjOther * impactForce * otherVM.Velocity;
            p.ApplyForce(force);
            otherVM.ApplyForce(-force * impactReduction);
        }

        #region Obsolete Code
        /*rb.AddForceAtPosition(-force, transform.position, ForceMode.Impulse);
        collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, collision.transform.position, ForceMode.Impulse);
        //collision.transform.forward = Vector3.Lerp(collision.transform.forward, collision.transform.forward + force, Time.deltaTime);
        //collision.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Vector3.Cross(collision.gameObject.transform.forward, force).sqrMagnitude * 100.0f, 0), ForceMode.Impulse);

        // Calculate the "torque" force
        /*float right = 0.0f;
        if (Vector3.Dot(force, collision.transform.forward) > 0)
            right = 1.0f;
        else
            right = -1.0f;

        float angle = torqueAngle * force.sqrMagnitude * right;
        Quaternion goal = Quaternion.Euler(collision.transform.rotation.eulerAngles.x, collision.transform.rotation.eulerAngles.y + angle, collision.transform.rotation.eulerAngles.z);

        // stop any current torque calculations
        StartCoroutine(ApplyTorque(goal, Mathf.Abs(angle) * timeStep, collision.gameObject));
        Debug.Log("Force: " + force.sqrMagnitude);*/
        #endregion

        ResetCar(other);

        Debug.Log("Trigger");

        collisionCount++;
    }

    public void CheckFallOff()
    {
        if(fallingStage)
        {
            //Debug.DrawRay(transform.position, -transform.up, Color.red,100.0f);
            if (!Physics.Raycast(transform.position, Vector3.down, 1f, floorMask))
            {
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                foreach (Collider col in gameObject.GetComponents<Collider>())
                {
                    col.isTrigger = false;
                }
                gameObject.GetComponent<Player>().LockRot = false;
                wasFalling = true;
            }
            else if(wasFalling)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                foreach (Collider col in gameObject.GetComponents<Collider>())
                {
                    col.isTrigger = true;
                }
                gameObject.GetComponent<Player>().LockRot = true;
                wasFalling = false;
            } 
        }
        else if((new Vector3(0,0.1f,0) - transform.position).sqrMagnitude > cM.ArenaRadius * cM.ArenaRadius)
        {
            rb.useGravity = true;
            foreach (Collider col in gameObject.GetComponents<Collider>())
            {
                col.isTrigger = false;
            }
            gameObject.GetComponent<Player>().LockRot = false;
        }
    }

    public void ResetCar(Collider other)
    {
        transform.position += -(p.Velocity);
    }

    public void ProjectileHit(Collider other)
    {
        p.ApplyForce(other.GetComponent<Cannonball>().velocity.normalized * cannonImpact);
        GameObject pSystem = transform.GetChild(4).gameObject;//HitByCannon Particle effect
        pSystem.transform.localPosition = transform.InverseTransformPoint(other.transform.position);
        StartCoroutine(Hit());
        Destroy(other.gameObject);
        StartCoroutine(Vibrate(0.15f, 3f, 0));
        StartCoroutine(Vibrate(0.15f, 3f, PlayerIndex.Two));
    }

    public void PunchHit(Collider other)
    {
        p.ApplyForce(other.GetComponent<RocketPunch>().velocity.normalized * (cannonImpact * 1.5f));
        Destroy(other.gameObject);
        StartCoroutine(Vibrate(0.15f, 3f, 0));
        StartCoroutine(Vibrate(0.15f, 3f, PlayerIndex.Two));
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
    IEnumerator Vibrate(float length, float intensity, PlayerIndex index)
    {
        GamePad.SetVibration(index, intensity, intensity);
        yield return new WaitForSeconds(length);
        GamePad.SetVibration(index, 0, 0);
    }
}
