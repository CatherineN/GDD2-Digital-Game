using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseArena : MonoBehaviour {

    // Use this for initialization
    System.Random rand;
    int randomInt;
    private Transform prevTransform;
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
        randomInt = rand.Next(0, transform.GetChild(0).transform.childCount);
        Transform faller = transform.GetChild(0).GetChild(randomInt);
        while(faller == prevTransform && transform.GetChild(0).transform.childCount > 1)
        {
            randomInt = rand.Next(0, transform.GetChild(0).transform.childCount);
            faller = transform.GetChild(0).GetChild(randomInt);
        }
        prevTransform = faller;
        faller.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        faller.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Fall());
    }
}
