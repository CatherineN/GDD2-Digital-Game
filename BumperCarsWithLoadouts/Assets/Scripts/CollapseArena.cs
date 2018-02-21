using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseArena : MonoBehaviour {

    // Use this for initialization
    System.Random rand;
    int randomInt;
    private bool matchStart = false;
	void Start ()
    {
        rand = new System.Random();
    }
	
	// Update is called once per frame
	void Update ()
    {
       if(matchStart == false && GameObject.Find("PlayerCar").GetComponent<Player>().enabled)
        {
            matchStart = true;
            StartCoroutine(Fall());
        }
	}
    
    IEnumerator Fall()
    {
        randomInt = rand.Next(0, transform.childCount);
        Transform faller = transform.GetChild(randomInt);
        faller.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(2);
        faller.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(Fall());
    }
}
