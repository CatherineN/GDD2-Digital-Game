using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombDropper : MonoBehaviour {

    // Use this for initialization
    public GameObject bomb;
    public Image slider;
    public Image ability;
    public Sprite sprite;
    private Animator anim;
    private int playerID;
    public float cooldownTime;
    private float cooldown;
    public AudioClip faucet;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.GetComponentInParent<Renderer>().material.color;
        //set the player id
        playerID = GetComponentInParent<BumperPhysics>().playerID;
        cooldown = cooldownTime;
        ability.sprite = sprite;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().material.color;
        cooldown += Time.deltaTime;
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")) && cooldown >= cooldownTime)&& gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject.Find("PlayerCar").GetComponent<AudioSource>().PlayOneShot(faucet);
                    GameObject bombInstance = Instantiate(bomb, gameObject.transform.position - (transform.up * 0.25f), new Quaternion(0, 0, 0, 0));
                    cooldown = 0.0f;
                    anim.SetTrigger("Active");
                    
                }
                break;
            case 2:
                //When the E key is pressed...
                if (((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")) && cooldown >= cooldownTime)&& gameObject.transform.parent.gameObject.GetComponent<BumperPhysics>().enabled == true)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject.Find("PlayerCar2").GetComponent<AudioSource>().PlayOneShot(faucet);
                    GameObject bombInstance = Instantiate(bomb, gameObject.transform.position - (transform.up * 0.25f), new Quaternion(0, 0, 0, 0));
                    cooldown = 0.0f;
                    anim.SetTrigger("Active");
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
