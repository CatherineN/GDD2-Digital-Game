using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //Debug.LogWarning(gameObject.GetComponent<Renderer>().material.color);
        gameObject.GetComponent<Renderer>().material.color = transform.GetComponentInParent<Renderer>().material.color;
        //Debug.LogWarning(gameObject.GetComponentInChildren<MeshRenderer>().gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().material.color;
    }
}
