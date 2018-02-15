using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPLauncher : MonoBehaviour {

    // Use this for initialization
    public GameObject rocketPunch;
    private int playerID;
	void Start ()
    {
        playerID = GetComponentInParent<Player>().playerID;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().materials[1].color;
        //cooldown += Time.deltaTime;
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3"))/* && cooldown >= cooldownTime*/)
                {
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.up * -1.5f), new Quaternion(0, 0, 0, 0));
                    rocketPunch.GetComponent<RocketPunch>().direction = -transform.up;
                    //StartCoroutine(Fire());
                    //cooldown = 0.0f;
                    //currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    //GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
            case 2:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2"))/* && cooldown >= cooldownTime*/)
                {
                    //Create a ROKETTO PUUUUNCH. The launcher is rotated, so we use the transform.up
                    GameObject rocketPunchInstance = Instantiate(rocketPunch, gameObject.transform.position + (transform.right * -1.5f), new Quaternion(0, 0, 0, 0));
                    rocketPunch.GetComponent<RocketPunch>().direction = -transform.right;
                    //StartCoroutine(Fire());
                    //cooldown = 0.0f;
                    //currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    //GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
        }
        //CalculateProgressBar();
    }
}

