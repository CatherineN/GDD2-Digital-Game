﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBLauncher : MonoBehaviour {

    // Use this for initialization
    public GameObject paintbang;
    private int playerID;
    void Start ()
    {
        playerID = GetComponentInParent<Player>().playerID;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().materials[1].color;
        switch (playerID)
        {
            case 1:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3")))
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject paintbangInstance = Instantiate(paintbang, gameObject.transform.position + (transform.up / 2.5f), new Quaternion(0, 0, 0, 0));
                    paintbangInstance.GetComponent<PaintBang>().direction = transform.up;
                    paintbangInstance.GetComponent<PaintBang>().parColor = gameObject.GetComponent<Renderer>().material.color;
                }
                break;
            case 2:
                //When the E key is pressed...
                if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("Fire2")))
                {
                    //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
                    GameObject paintbangInstance = Instantiate(paintbang, gameObject.transform.position + (transform.up / 2.5f), new Quaternion(0, 0, 0, 0));
                }
                break;
        }
    }
}

