using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honking : MonoBehaviour {
    public AudioClip currentHonk;
    public AudioClip[] honks;
    public bool isPlayerOne;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ListenForHonk();
	}
    void ListenForHonk()
    {
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Honk1")) && isPlayerOne)
        {
            currentHonk = honks[Random.Range(0, honks.Length - 1)];
            gameObject.GetComponent<AudioSource>().PlayOneShot(currentHonk);
        }
        else if((Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("Honk2")) && !isPlayerOne)
        {
            currentHonk = honks[Random.Range(0, honks.Length - 1)];
            gameObject.GetComponent<AudioSource>().PlayOneShot(currentHonk);
        }
    }
}
