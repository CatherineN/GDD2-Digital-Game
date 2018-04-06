using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

    // Use this for initialization
    public GameObject cannonball;
    public Image slider;
    public Image ability;
    public Sprite sprite;
    private int playerID;
    public float cooldownTime;
    private float cooldown;
    public AudioClip currentFire;
    public AudioClip[] fireSounds = new AudioClip[5];
    void Start ()
    {
        //gameObject.GetComponent<Renderer>().material.color = transform.GetComponentInParent<Renderer>().materials[1].color;
        //set the player id
        playerID = GetComponentInParent<Player>().playerID;
        cooldown = cooldownTime;
        ability.sprite = sprite;
        fireSounds[0] = Resources.Load("cannon sound 1") as AudioClip;
        fireSounds[1] = Resources.Load("cannon sound 2") as AudioClip;
        fireSounds[2] = Resources.Load("cannon sound 3") as AudioClip;
        fireSounds[3] = Resources.Load("cannon sound 4") as AudioClip;
        fireSounds[4] = Resources.Load("cannon sound 5") as AudioClip;
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
                if (((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")) && cooldown >= cooldownTime)&& gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject cannonballInstance = Instantiate(cannonball, gameObject.transform.position + (transform.right * -1.5f), new Quaternion(0, 0, 0, 0));
                    cannonballInstance.GetComponent<Cannonball>().direction = -transform.right;
                    StartCoroutine(Fire());
                    cooldown = 0.0f;

                    currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
            case 2:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")) && cooldown >= cooldownTime)&& gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject cannonballInstance = Instantiate(cannonball, gameObject.transform.position + (transform.right * -1.5f), new Quaternion(0, 0, 0, 0));
                    cannonballInstance.GetComponent<Cannonball>().direction = -transform.right;
                    StartCoroutine(Fire());
                    cooldown = 0.0f;

                    currentFire = fireSounds[Random.Range(0, fireSounds.Length - 1)];
                    GameObject.Find("PlayerCar2").GetComponent<AudioSource>().PlayOneShot(currentFire);
                }
                break;
        }
        CalculateProgressBar();
    }

    IEnumerator Fire()
    {
        GameObject p = transform.GetChild(0).gameObject;
        ParticleSystem pSystem = p.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = pSystem.emission;
        for (int i = 0; i < 20; i++)
        {
            em.enabled = true;
            yield return null;
        }
        em.enabled = false;
    }

    public void CalculateProgressBar()
    {
        float percent;
        if(cooldown >= cooldownTime)
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
