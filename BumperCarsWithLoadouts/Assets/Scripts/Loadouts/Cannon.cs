using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    // Use this for initialization
    public GameObject cannonball;
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
        //When the E key is pressed...
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3"))
        {
            //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
            GameObject cannonballInstance = Instantiate(cannonball, gameObject.transform.position + (transform.up * 1.5f), new Quaternion(0,0,0,0));
            cannonballInstance.GetComponent<Cannonball>().direction = transform.up;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        GameObject p = transform.GetChild(0).gameObject;
        ParticleSystem pSystem = p.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = pSystem.emission;
        for (int i = 0; i < 20; i++)
        {
            em.enabled = true;
            yield return null;
        }
        em.enabled = false;
    }
}
