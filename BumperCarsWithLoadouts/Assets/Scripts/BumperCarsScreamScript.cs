using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCarsScreamScript : MonoBehaviour {
    public AudioClip[] screams;
    public AudioClip currentScream;
	// Use this for initialization
	void Start () {
        currentScream = screams[Random.Range(0, screams.Length - 1)];
        gameObject.GetComponent<AudioSource>().PlayOneShot(currentScream);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
