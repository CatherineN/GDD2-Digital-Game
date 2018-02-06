using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseArena : MonoBehaviour {

    // Use this for initialization
    System.Random rand;
    int randomInt;
	void Start ()
    {
        rand = new System.Random();
        StartCoroutine(Fall());
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}
    
    IEnumerator Fall()
    {
        randomInt = rand.Next(0, transform.childCount);
        transform.GetChild(randomInt).GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(2);
        transform.GetChild(randomInt).GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(Fall());
    }
}
