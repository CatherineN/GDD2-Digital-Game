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
    public float cooldownTime;
    private float cooldown;
    private int playerID;
	void Start ()
    {
        cooldown = cooldownTime;
        playerID = GetComponentInParent<Player>().playerID;
        ability.sprite = sprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().materials[1].color;
        cooldown += Time.deltaTime;
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")) && cooldown >= cooldownTime) && gameObject.transform.parent.gameObject.GetComponent<Player>().enabled == true)
                {
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.up * -1.5f), new Quaternion(0, 0, 0, 0));
                    rocketPunchInstance.GetComponent<RocketPunch>().direction = -transform.up;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentCarID = gameObject.GetComponentInParent<Player>().playerID;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentVelocity = gameObject.GetComponentInParent<Player>().Velocity;
                   //StartCoroutine(Fire());
                   cooldown = 0.0f;
                    //currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    //GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
            case 2:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")) && cooldown >= cooldownTime)  && gameObject.transform.parent.gameObject.GetComponent<Player>().enabled == true)
                {
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.up * -1.5f), new Quaternion(0, 0, 0, 0));
                    rocketPunchInstance.GetComponent<RocketPunch>().direction = -transform.up;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentCarID = gameObject.GetComponentInParent<Player>().playerID;
                    rocketPunchInstance.GetComponent<RocketPunch>().parentVelocity = gameObject.GetComponentInParent<Player>().Velocity;
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
}

