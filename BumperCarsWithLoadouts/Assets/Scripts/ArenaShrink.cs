﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If you want to use this on an arena make sure it is inside of an empty gameobject with following traits:
/// Scale - (1,1,1)
/// Position - (0,0,0)
/// Name - Arena
/// Child - Model of the arena 
/// Origin point is in the middle of the top of the arena model
/// </summary>


public class ArenaShrink : MonoBehaviour {

    private Vector3 defaultSize;//what the arena starts at in terms of local scale
    private Vector3 minSize;//the smallest scale the arena will shrink to
    public float shrinkSpeed = .1f;//how quickly the arena shrinks
    public float secsTilShrink = 10;//the delay in seconds until the arena begins shrinking
    public float minSizef = .1f;//the public facing variable that determines how small the arena will end up being
    private float startTime;

    private void Awake()
    {
        //make sure that the default size of the stage is correct at the start of the match
        transform.localScale = Vector3.one;
        //make the timer reflect the seconds until the arena shrinks
        GameObject.Find("Canvas").GetComponent<Timer>().timeLeft = secsTilShrink;
        //make the timer reflect the curretn status of the stage shrinking
        GameObject.Find("Canvas").GetComponent<Timer>().endMessage = "WARNING: Arena Shrinking!";
    }

    // Use this for initialization
    void Start () {
        minSize = new Vector3(minSizef, minSizef, minSizef);
        defaultSize = Vector3.one;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float timeElasped = Time.time - startTime;
        //if the match has been going for too long, shrink the arena
		if(timeElasped > secsTilShrink)
        {
            float percent = (timeElasped - secsTilShrink) *shrinkSpeed/Vector3.Distance(defaultSize, minSize);
            transform.localScale = Vector3.Lerp(defaultSize, minSize, percent);
        }
        
	}

    private void OnDisable()
    {
        //make sure that the default size of the stage is correct at the end of the match
        defaultSize = transform.localScale = Vector3.one;
    }
}
