﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPieceDiscard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -60)
        {
            Destroy(gameObject);
        }
	}
}
