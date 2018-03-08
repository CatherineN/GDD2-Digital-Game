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
    public float fallTime;
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
        if (transform.GetChild(0).transform.childCount == 0) yield return null;
        randomInt = rand.Next(0, transform.GetChild(0).transform.childCount);
        Transform faller = transform.GetChild(0).GetChild(randomInt);
        while(faller == prevTransform && transform.GetChild(0).transform.childCount > 1)
        {
            randomInt = rand.Next(0, transform.GetChild(0).transform.childCount);
            faller = transform.GetChild(0).GetChild(randomInt);
        }
        prevTransform = faller;
        faller.gameObject.GetComponent<FlashColor>().enabled = true;
        yield return new WaitForSeconds(fallTime);
        faller.gameObject.GetComponent<FlashColor>().enabled = false;
        faller.GetComponent<Rigidbody>().useGravity = true;
        faller.GetComponent<Rigidbody>().isKinematic = false;
        faller.GetComponent<Collider>().enabled = false;
        faller.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        yield return new WaitForSeconds(fallTime);
        StartCoroutine(Fall());
    }
}
