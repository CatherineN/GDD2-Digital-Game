using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public float velocity = 2f;
    public bool decoration = false;
    private bool falling;
	// Use this for initialization
	void Start ()
    {
        falling = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!decoration)
        {
            if (transform.position.y > 0.13f)
            {
                transform.position += Vector3.down * velocity;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0.13f, transform.position.z);
                Destroy(this);
            }
        }
		else
        {
            if (transform.position.y > -100f)
            {
                transform.position += Vector3.down * velocity;
            }
            else
                Destroy(gameObject);
        }
	}
}
