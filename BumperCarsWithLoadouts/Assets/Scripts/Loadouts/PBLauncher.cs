using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PBLauncher : MonoBehaviour {

    // Use this for initialization
    public GameObject paintbang;
    private int playerID;

    public Image slider;
    public Image ability;
    public Sprite sprite;
    public float cooldownTime;
    private float cooldown;
    void Start ()
    {
        playerID = GetComponentInParent<Player>().playerID;
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().materials[1].color;
        cooldown = cooldownTime;
        ability.sprite = sprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
        cooldown += Time.deltaTime;
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")) && cooldown >= cooldownTime)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject paintbangInstance = Instantiate(paintbang, gameObject.transform.position + (transform.up / 2.5f), new Quaternion(0, 0, 0, 0));
                    paintbangInstance.GetComponent<PaintBang>().direction = transform.up;
                    paintbangInstance.GetComponent<PaintBang>().parColor = gameObject.GetComponent<Renderer>().material.color;
                    cooldown = 0.0f;
                }
                break;
            case 2:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")) && cooldown >= cooldownTime)
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject paintbangInstance = Instantiate(paintbang, gameObject.transform.position + (transform.up / 2.5f), new Quaternion(0, 0, 0, 0));
                    cooldown = 0.0f;
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

