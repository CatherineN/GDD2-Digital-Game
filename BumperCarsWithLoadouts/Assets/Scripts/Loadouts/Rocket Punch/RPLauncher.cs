using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPLauncher : MonoBehaviour {

    // Use this for initialization
    public GameObject rocketPunch;
    public Image slider;
    public Image ability;
    public Sprite sprite;
    private float targetAngle;
    public float cooldownTime;
    private float cooldown;
    private int playerID;
    private List<GameObject> carList;
    private GameObject targetCar;
    public GameObject reticle;

    private Animator anim;

    void Start ()
    {
        cooldown = cooldownTime;
        playerID = GetComponentInParent<Player>().playerID;
        ability.sprite = sprite;
        carList = GameObject.Find("SceneManager").GetComponent<CarManager>().Cars;
        targetAngle = 45f;
        targetCar = null;
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().materials[1].color;
        cooldown += Time.deltaTime;
        FindTarget();
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")) && cooldown >= cooldownTime) && gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true && targetCar != null)
                {
                    anim.SetTrigger("Active");
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.forward * 1.5f), new Quaternion(0, 0, 0, 0));
                    Debug.Log(transform.right);
                    Debug.Log(transform.up);
                    rocketPunchInstance.GetComponent<RocketPunch>().direction = transform.forward;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentCarID = gameObject.GetComponentInParent<Player>().playerID;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentVelocity = gameObject.GetComponentInParent<Player>().Velocity;
                    rocketPunchInstance.GetComponent<RocketPunch>().carToSeek = targetCar;
                    rocketPunchInstance.GetComponent<RocketPunch>().reticle = reticle;
                    //StartCoroutine(Fire());
                    cooldown = 0.0f;
                    //currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    //GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
            case 2:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")) && cooldown >= cooldownTime)  && gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true && targetCar != null)
                {
                    anim.SetTrigger("Active");
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.forward * 1.5f), new Quaternion(0, 0, 0, 0));
                    rocketPunchInstance.GetComponent<RocketPunch>().direction = transform.forward;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentCarID = gameObject.GetComponentInParent<Player>().playerID;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentVelocity = gameObject.GetComponentInParent<Player>().Velocity;
                    rocketPunchInstance.GetComponent<RocketPunch>().carToSeek = targetCar;
                    rocketPunchInstance.GetComponent<RocketPunch>().reticle = reticle;
                    //StartCoroutine(Fire());
                    cooldown = 0.0f;
                    //currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    //GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
        }
        CalculateProgressBar();
    }
    public void CalculateProgressBar()
    {
        float percent;
        if (cooldown >= cooldownTime)
        {
            percent = 1.0f;
        }
        else
        {
            percent = cooldown / cooldownTime;
        }

        slider.fillAmount = percent;
    }

    private void FindTarget()
    {
        // only find a target if the rocket is ready to go
        if (cooldown < cooldownTime) return;
        GameObject target = null;
        float closestMag = float.MaxValue;

        // loop through the cars to find the closest in range
        foreach(GameObject other in carList)
        {
            if (other == transform.parent.gameObject) continue;

            Vector3 vecTo = other.transform.position - transform.position;
            float angle = Vector3.Angle(vecTo, transform.parent.transform.forward);
            if(angle <= targetAngle)
            {
                if(vecTo.sqrMagnitude < closestMag)
                {
                    target = other;
                    closestMag = vecTo.sqrMagnitude;
                }
            }
        }

        if (target != null)
        {
            targetCar = target;
            reticle.SetActive(true);
            reticle.GetComponent<TargetUI>().target = target;
        }
        else
        {
            targetCar = null;
            reticle.SetActive(false);
        }
    }
}

