using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float amount;
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = 0;
        float v = 0;
        if (Input.GetKey(KeyCode.A))
        {
            h = -amount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            h = amount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            v = amount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            v = -amount * Time.deltaTime;
        }

        rb.rotation = Quaternion.identity;
        rb.AddTorque(transform.up * h);
        rb.AddTorque(transform.right * v);
    }
}
