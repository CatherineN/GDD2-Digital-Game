using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public float velocity = 2f;
    public bool decoration = false;
    private bool falling;
    private Vector3 randDir;
    private float randF;
	// Use this for initialization
	void Start ()
    {
        falling = true;
        randDir = Random.onUnitSphere;
        randF = Random.Range(-10f, 10f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!decoration)
        {
            if (transform.position.y > 0.13f)
            {
                transform.position += Vector3.down * velocity;
                transform.Rotate(transform.position + randDir, randF);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0.13f, transform.position.z);
                transform.rotation = Quaternion.identity;
                Destroy(this);
            }
        }
		else
        {
            if (transform.position.y > -100f)
            {
                transform.position += Vector3.down * velocity;
                transform.Rotate(transform.position + randDir, randF);
            }
            else
                Destroy(gameObject);
        }
	}
}
