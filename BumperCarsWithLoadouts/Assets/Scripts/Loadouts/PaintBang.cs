using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBang : MonoBehaviour {

    // Use this for initialization
    private float speed;
    public Vector3 velocity;
    public Vector3 direction;
    public Color parColor;
    void Start ()
    {
        gameObject.GetComponent<Renderer>().material.color = parColor;
        speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
